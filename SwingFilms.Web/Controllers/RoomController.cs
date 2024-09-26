using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Services.Features.Room.Commands;
using SwingFilms.Services.Features.Room.Queries;

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
    /// Получение комнат пользователя
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Комнаты</returns>
    [HttpGet(nameof(GetRooms), Name = nameof(GetRooms))]
    public async Task<ActionResult> GetRooms(GetRoomsQuery command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Получение комнаты
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Комната</returns>
    [HttpGet(nameof(GetRoom), Name = nameof(GetRoom))]
    public async Task<ActionResult> GetRoom(GetRoomQuery command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Создание комнаты
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost(nameof(CreateRoom), Name = nameof(CreateRoom))]
    public async Task<ActionResult> CreateRoom(CreateRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }  
    
    /// <summary>
    /// Удаление комнаты
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete(nameof(DeleteRoom), Name = nameof(DeleteRoom))]
    public async Task<ActionResult> DeleteRoom(DeleteRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }  
    
    /// <summary>
    /// Редактирование параметра комнаты
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost(nameof(EditRoomParameter), Name = nameof(EditRoomParameter))]
    public async Task<ActionResult> EditRoomParameter(EditRoomParameterCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Получение истории пользователя в комнате
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список фильмов</returns>
    [HttpGet(nameof(GetRoomUserHistory), Name = nameof(GetRoomUserHistory))]
    public async Task<ActionResult> GetRoomUserHistory(GetRoomUserHistoryQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Получение взаимных лайков в комнате
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <param name="cancellationToken">Токено отмены</param>
    /// <returns>Список фильмов</returns>
    [HttpGet(nameof(GetRoomMatches), Name = nameof(GetRoomMatches))]
    public async Task<ActionResult> GetRoomMatches(GetRoomMatchesQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Очистка истории комнаты
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost(nameof(ClearRoomHistory), Name = nameof(ClearRoomHistory))]
    public async Task<ActionResult> ClearRoomHistory(ClearRoomHistoryCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}