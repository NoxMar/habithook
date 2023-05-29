using System.Net.Http.Json;
using HabitHook.HabitManagement.Domain.Contracts.Habits;
using HabitHook.HabitManagement.Domain.Contracts.Habits.ListHabits;
using MapsterMapper;
using MediatR;

namespace HabitHook.HabitManagement.Domain.ListHabit;

public class ListHabitsQueryHandler : IRequestHandler<ListHabitsQuery, IEnumerable<HabitDto>>
{
    private const string Path = "/api/habits";
    private readonly IHttpClientFactory _httpClientFactory;

    public ListHabitsQueryHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<HabitDto>> Handle(ListHabitsQuery request, CancellationToken cancellationToken)
    {
        var client = _httpClientFactory.CreateClient("ApiClient");
        var test = await client.GetAsync(Path, cancellationToken);
        var response = await client.GetFromJsonAsync<List<HabitDto>>(Path, cancellationToken);
        if (response is null)
        {
            throw new HttpRequestException($"request to \"{Path}\" failed");
        }

        return response;
    }
}