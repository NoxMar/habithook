using HabitHook.HabitManagement.Domain.Contracts.Habits.ListHabits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HabitHook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HabitsController : ControllerBase
{
    private readonly IMediator _mediator;

    public HabitsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<HabitDto>> AddHabit([FromBody] HabitForCreationDto habitForCreation)
    {
        AddHabitCommand command = new (habitForCreation);
        var response = await _mediator.Send(command);
        return CreatedAtRoute(nameof(GetHabit),
            new { response.Id },
            response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HabitDto>>> GetAllHabits()
    {
        var response = await _mediator.Send(new ListHabitsQuery(new ListHabitsQueryDto()));
        return Ok(response);
    }

    [HttpGet("{id:guid}", Name = nameof(GetHabit))]
    public Task<ActionResult<HabitDto>> GetHabit( Guid id)
    {
        var result = Ok(new HabitDto { Id = id });
        return Task.FromResult<ActionResult<HabitDto>>(result);
    }

    [HttpDelete("{id:guid}", Name = nameof(DeleteHabit))]
    public async Task<IActionResult> DeleteHabit(Guid id) 
        => await _mediator.Send(new DeleteHabitCommand(id)) ? Ok() : NotFound();
}