namespace HabitHook.Common.Services;

public interface IDateTimeProvider
{
    DateTime DateTimeUtcNow { get; }
}

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime DateTimeUtcNow => DateTime.UtcNow;
}