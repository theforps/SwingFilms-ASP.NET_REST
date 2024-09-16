using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Services.Features.Room.Commands;

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

    [HttpPost(nameof(GetUserHistory), Name = nameof(GetUserHistory))]
    public async Task<ActionResult> GetUserHistory(GetUserHistoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    // [HttpPost(nameof(GetRoomMatches), Name = nameof(GetRoomMatches))]
    // public async Task<ActionResult> GetRoomMatches([FromBody] Guid roomId,
    //     CancellationToken cancellationToken)
    // {
    //     var result = await _historyRoomRepository.GetRoomMatches(roomId, cancellationToken);
    //     
    //     return Ok(result);
    // }
    //
    // [HttpPost(nameof(ClearRoomHistory), Name = nameof(ClearRoomHistory))]
    // public async Task<ActionResult> ClearRoomHistory([FromBody] Guid roomId,
    //     CancellationToken cancellationToken)
    // {
    //     await _historyRoomRepository.ClearRoomHistory(roomId, cancellationToken);
    //     
    //     return Ok();
    // }
}