using MediatR;

namespace HabitHook.HabitManagement.Domain.Contracts.Habits;

public record DeleteHabitCommand(Guid Id) : IRequest<bool>;