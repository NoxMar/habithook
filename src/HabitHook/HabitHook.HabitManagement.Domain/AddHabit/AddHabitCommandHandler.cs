using HabitHook.HabitManagement.Database;
using HabitHook.HabitManagement.Domain.Contracts.Habits;
using HabitHook.HabitManagement.Domain.Contracts.Habits.AddHabit;
using HabitHook.HabitManagement.Domain.Core.Habits;
using MapsterMapper;
using MediatR;

namespace HabitHook.HabitManagement.Domain.AddHabit;

public sealed class AddHabitCommandHandler : IRequestHandler<AddHabitCommand, HabitDto>
{
    private readonly IMapper _mapper;
    private readonly HabitContext _habitContext;

    public AddHabitCommandHandler(IMapper mapper, HabitContext habitContext)
    {
        _mapper = mapper;
        _habitContext = habitContext;
    }

    public async Task<HabitDto> Handle(AddHabitCommand request, CancellationToken cancellationToken)
    {
        var habitToAdd = _mapper.Map<HabitForCreation>(request.HabitToAdd);
        var habit = Habit.Create(habitToAdd);
        _habitContext.Habits.Add(habit);
        await _habitContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<HabitDto>(habit);
    }
}