using System.Reflection;
using Mapster;
using MapsterMapper;
using Serilog;

using HabitHook.Config;
using HabitHook.Common;
using HabitHook.HabitManagement.Database;
using Microsoft.EntityFrameworkCore;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting API");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();
    
    // Add Mapster
    var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
// scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
    typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
// register the mapper as Singleton service for my application
    var mapperConfig = new Mapper(typeAdapterConfig);
    builder.Services.AddSingleton<IMapper>(mapperConfig);

    builder.Services.RegisterServices();
    
    builder.Services.AddMediatR();
    
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddDbContext<HabitContext>(cfg =>
            cfg.UseInMemoryDatabase("DevDb"));
    }
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAnyOrigin", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
    
    builder.Services.AddHealthChecks();
    
    
    builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();
    app.MapHealthChecks("/api/health");
// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseSerilogRequestLogging();

    app.UseCors("AllowAnyOrigin");

    app.UseAuthorization();

    app.MapControllers();

    app.MapGet("/", () => "Hello World!");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}