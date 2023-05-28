namespace HabitHook.Config;

public static class MediatRConfig
{
    public static void AddMediatR(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Habit>();
        });
    }
}