using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Services.Features.Room.Commands;

namespace SwingFilms.Controllers;

[Route("[controller]")]
[Authorize]
public class RoomController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RoomController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Создание комнаты
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost(nameof(CreateRoom), Name = nameof(CreateRoom))]
    public async Task<ActionResult> CreateRoom(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}