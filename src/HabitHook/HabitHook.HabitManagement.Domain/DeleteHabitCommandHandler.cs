using HabitHook.HabitManagement.Database;
using HabitHook.HabitManagement.Domain.Contracts.Habits;
using HabitHook.HabitManagement.Domain.Core.Habits;
using MediatR;

namespace HabitHook.HabitManagement.Domain;

public class DeleteHabitCommandHandler : IRequestHandler<DeleteHabitCommand, bool>
{
    private readonly HabitContext _habitContext;

    public DeleteHabitCommandHandler(HabitContext habitContext)
    {
        _habitContext = habitContext;
    }

    public async Task<bool> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
    {
        var habit = await _habitContext.Habits.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);
        if (habit is null) return false;
        _habitContext.Habits.Remove(habit);
        return await _habitContext.SaveChangesAsync(cancellationToken) == 1;
    }
}