using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Services.Features.Room.Commands;
using SwingFilms.Services.Features.Room.Queries;

namespace SwingFilms.Controllers;

[Route("[controller]")]
[Authorize]
public class HistoryController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public HistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(nameof(GetGroupUserHistory), Name = nameof(GetGroupUserHistory))]
    public async Task<ActionResult> GetGroupUserHistory(GetGroupUserHistoryQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet(nameof(GetRoomMatches), Name = nameof(GetRoomMatches))]
    public async Task<ActionResult> GetRoomMatches(GetGroupMatchesQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpPost(nameof(ClearRoomHistory), Name = nameof(ClearRoomHistory))]
    public async Task<ActionResult> ClearRoomHistory(ClearRoomHistoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}