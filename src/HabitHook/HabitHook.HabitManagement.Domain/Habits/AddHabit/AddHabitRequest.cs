using MapsterMapper;
using MediatR;

namespace HabitHook.HabitManagement.Domain.Habits.AddHabit;

public sealed class AddHabitCommandHandler : IRequestHandler<AddHabitCommand, HabitDto>
{
    private readonly IMapper _mapper;

    public AddHabitCommandHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<HabitDto> Handle(AddHabitCommand request, CancellationToken cancellationToken)
    {
        var habitToAdd = _mapper.Map<HabitForCreation>(request.HabitToAdd);
        var habit = Habit.Create(habitToAdd);

        return Task.FromResult(_mapper.Map<HabitDto>(habit));
    }
}