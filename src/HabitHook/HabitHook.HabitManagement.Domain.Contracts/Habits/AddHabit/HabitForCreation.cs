namespace HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;

public record HabitForCreation (
    string Name,
    int TargetValue);