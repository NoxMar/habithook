using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HabitHook.HabitManagement.Client;
using HabitHook.HabitManagement.Client.Common;
using HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(AddHabitCommand).Assembly);
});

builder.Services.AddHttpClient("ApiClient",
    client => client.BaseAddress
        = new Uri(builder.Configuration["ApiClient:BaseUri"]
                  ?? throw new ConfigurationException("\"ApiClient.BaseUri\" not set")));

await builder.Build().RunAsync();
