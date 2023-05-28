namespace HabitHook.HabitManagement.Domain.Contracts.Habits;

public class HabitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public int TargetValue { get; set; }
    public DateTime CreatedOn { get; set; }
}