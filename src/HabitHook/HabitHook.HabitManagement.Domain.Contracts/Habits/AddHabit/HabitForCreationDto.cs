namespace HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;

public record HabitForCreationDto (
    string Name,
    int TargetValue);