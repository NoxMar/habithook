namespace HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;

public record HabitForCreationDto
{
    public string Name { get; set; } = string.Empty;
    public int TargetValue { get; set; } = 1;
}