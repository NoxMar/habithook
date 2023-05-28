using System.ComponentModel.DataAnnotations;
using HabitHook.Domain.Common;
using HabitHook.HabitManagement.Domain.Habits.AddHabit;

namespace HabitHook.HabitManagement.Domain.Habits;

public class Habit : BaseEntity
{
    [Required] public string Name { get; private set; } = string.Empty;
    
    [Required] public int TargetValue { get; private set; }

    public static Habit Create(HabitForCreation toCreate)
    {
        var newHabit = new Habit();
        newHabit.Name = toCreate.Name;
        newHabit.TargetValue = toCreate.TargetValue;
        
        newHabit.QueueDomainEvent(new HabitCreated(newHabit));
        return newHabit;
    }
}