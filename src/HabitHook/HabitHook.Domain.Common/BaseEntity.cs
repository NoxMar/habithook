using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HabitHook.Domain.Common;

public abstract class BaseEntity
{
    [Key] public Guid Id { get; private set; } = Guid.NewGuid();

    public DateTime CreatedOn { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTime? LastModifiedOn { get; private set; }
    public string? LastModifiedBy { get; private set; }
    public bool IsDeleted { get; private set; }
    
    [NotMapped]
    public List<DomainEvent> DomainEvents { get; } = new();

    public void UpdateCreationProperties(DateTime createdOn, string? createdBy)
    {
        CreatedOn = createdOn;
        CreatedBy = createdBy;
    }

    public void UpdateModifiedProperties(DateTime? lastModified, string? modifiedBy)
    {
        LastModifiedOn = lastModified;
        LastModifiedBy = modifiedBy;
    }

    public void UpdateIsDeleted(bool isDeleted)
    {
        IsDeleted = isDeleted;
    }

    public void QueueDomainEvent(DomainEvent @event)
    {
        if (!DomainEvents.Contains(@event))
        {
            DomainEvents.Add(@event);
        }
    }
}