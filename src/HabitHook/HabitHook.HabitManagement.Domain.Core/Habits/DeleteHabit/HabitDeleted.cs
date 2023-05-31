using HabitHook.Domain.Common;

namespace HabitHook.HabitManagement.Domain.Core.Habits.DeleteHabit;

public class HabitDeleted : DomainEvent
{
    public HabitDeleted(Habit habit)
    {
        Habit = habit;
    }

    public Habit Habit { get; private init; }
}