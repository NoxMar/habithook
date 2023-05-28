using HabitHook.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HabitHook.Common;

public static class Register
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICurrentUserService, MockCurrentUserService>();
    }
}