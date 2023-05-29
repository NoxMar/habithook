using System.Net.Http.Json;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using HabitHook.HabitManagement.Domain.Contracts.Habits;
using HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;
using MediatR;

namespace HabitHook.HabitManagement.Client.Habit.AddHabit;

public class AddHabitHandler : IRequestHandler<AddHabitCommand, HabitDto>
{
    private const string Path = "/api/habits";
    private readonly IHttpClientFactory _httpClientFactory;

    public AddHabitHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    
    public async Task<HabitDto> Handle(AddHabitCommand request, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync(Path, request.HabitToAdd, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"request to \"{Path}\" failed");
        }

        var responseDeserialized = await response.Content.ReadFromJsonAsync<HabitDto>(cancellationToken: cancellationToken)
                                   ?? throw new SerializationException(
                                       $"Could not deserialize {response.Content} despite code indicating success");
        return responseDeserialized;
    }
}