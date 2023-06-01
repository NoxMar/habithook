using HabitHook.HabitManagement.Domain.Contracts.Habits;
using MediatR;

namespace HabitHook.HabitManagement.Client.Habit.DeleteHabit;

public class DeleteHabitHandler : IRequestHandler<DeleteHabitCommand, bool>
{
    private const string PathTemplate = "/api/habits/{id}";

    private readonly IHttpClientFactory _httpClientFactory;

    public DeleteHabitHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        var actualUrl = PathTemplate.Replace("{id}",
            Uri.EscapeDataString(request.Id.ToString()));
        var client = _httpClientFactory.CreateClient("ApiClient");
        var response = await client.DeleteAsync(actualUrl, cancellationToken);
        return response.IsSuccessStatusCode;
    }
}