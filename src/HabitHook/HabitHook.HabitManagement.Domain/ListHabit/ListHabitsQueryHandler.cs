using HabitHook.HabitManagement.Database;
using HabitHook.HabitManagement.Domain.Contracts.Habits;
using HabitHook.HabitManagement.Domain.Contracts.Habits.ListHabits;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HabitHook.HabitManagement.Domain.ListHabit;

public class ListHabitsQueryHandler : IRequestHandler<ListHabitsQuery, IEnumerable<HabitDto>>
{
    private readonly IMapper _mapper;
    private readonly HabitContext _habitContext;

    public ListHabitsQueryHandler(IMapper mapper, HabitContext habitContext)
    {
        _mapper = mapper;
        _habitContext = habitContext;
    }

    public async Task<IEnumerable<HabitDto>> Handle(ListHabitsQuery request, CancellationToken cancellationToken) =>
        await _habitContext
            .Habits
            .Select(h => _mapper.Map<HabitDto>(h))
            .ToListAsync(cancellationToken);
}