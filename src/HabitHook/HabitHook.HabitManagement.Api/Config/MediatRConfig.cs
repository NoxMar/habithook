using HabitHook.HabitManagement.Domain.AddHabit;

namespace HabitHook.Config;

public static class MediatRConfig
{
    public static void AddMediatR(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(Habit).Assembly,
                typeof(AddHabitCommand).Assembly,
                typeof(AddHabitCommandHandler).Assembly);
        });
    }
}