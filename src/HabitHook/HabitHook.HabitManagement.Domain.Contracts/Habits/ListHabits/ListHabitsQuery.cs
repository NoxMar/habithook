using MediatR;

namespace HabitHook.HabitManagement.Domain.Contracts.Habits.ListHabits;

public record ListHabitsQuery(ListHabitsQueryDto QueryDto) : IRequest<IEnumerable<HabitDto>>;