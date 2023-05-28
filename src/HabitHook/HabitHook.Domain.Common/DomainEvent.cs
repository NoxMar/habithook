using System.Reflection;
using Mediator;

namespace HabitHook.Domain.Common;

public class DomainEvent : INotification, IEquatable<DomainEvent>
{
    private List<PropertyInfo>? _properties;
    private List<FieldInfo>? _fields;

    public bool Equals(DomainEvent? other)
    {
        return Properties
                   .All(p => Equals(p.GetValue(this, null), p.GetValue(other, null)))
               && Fields
                   .All(f => Equals(f.GetValue(this), f.GetValue(other)));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals(obj as DomainEvent);
    }

    private IEnumerable<PropertyInfo> Properties => _properties ??= GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetCustomAttribute<IgnoreMemberAttribute>() is null)
            .ToList();

    private IEnumerable<FieldInfo> Fields => _fields ??= GetType()
        .GetFields(BindingFlags.Instance | BindingFlags.Public)
        .Where(f => f.GetCustomAttribute<IgnoreMemberAttribute>() is null)
        .ToList();


    public override int GetHashCode()
    {
        // This intentionally might cause overflow which is normal for hashes
        
        int hash = Properties
            .Select(prop => prop.GetValue(this, null))
            .Aggregate(17, (current, value) => HashValue(current, value));

        return Fields.Select(field => field.GetValue(this))
            .Aggregate(hash, (current, value) => HashValue(current, value));
    }

    private static int HashValue(int seed, object? value)
    {
        var currentHash = value?.GetHashCode() ?? 0;
        return seed * 23 + currentHash;
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    private class IgnoreMemberAttribute : Attribute
    {
    }
}