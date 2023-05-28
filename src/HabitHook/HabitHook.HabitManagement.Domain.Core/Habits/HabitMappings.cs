using Mapster;

using HabitHook.HabitManagement.Domain.Core.Habits.AddHabit;

namespace HabitHook.HabitManagement.Domain.Core.Habits;

public sealed class HabitMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Habit, HabitDto>();
        config.NewConfig<HabitForCreation, Habit>()
            .TwoWays();
        config.NewConfig<HabitForCreationDto, Habit>()
            .TwoWays();
    }
}