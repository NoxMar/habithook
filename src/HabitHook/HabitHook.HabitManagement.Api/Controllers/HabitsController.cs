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
            new { id = response.Id },
            response);
    }
    
    [HttpGet("{id:guid}")]
    public Task<ActionResult<HabitDto>> GetHabit( Guid id)
    {
        var result = Ok(new HabitDto { Id = id });
        return Task.FromResult<ActionResult<HabitDto>>(result);
    }
}