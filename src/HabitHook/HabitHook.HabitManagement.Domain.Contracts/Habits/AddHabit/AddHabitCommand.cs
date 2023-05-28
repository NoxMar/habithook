using MediatR;

namespace HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;

public record AddHabitCommand(HabitForCreationDto HabitToAdd)  : IRequest<HabitDto>;