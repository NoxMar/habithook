using HabitHook.Domain.Common;

namespace HabitHook.HabitManagement.Domain.Core.Habits.AddHabit;

public class HabitCreated : DomainEvent
{
    public Habit Habit { get; private init; }

    public HabitCreated(Habit habit)
    {
        Habit = habit;
    }
}