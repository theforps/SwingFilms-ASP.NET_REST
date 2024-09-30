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
    /// <param name="query">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Комнаты</returns>
    [HttpGet(nameof(GetRooms), Name = nameof(GetRooms))]
    public async Task<ActionResult> GetRooms(GetRoomsQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Получение комнаты
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Комната</returns>
    [HttpGet(nameof(GetRoom), Name = nameof(GetRoom))]
    public async Task<ActionResult> GetRoom(GetRoomQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
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
    [HttpPut(nameof(EditParameterRoom), Name = nameof(EditParameterRoom))]
    public async Task<ActionResult> EditParameterRoom(EditParameterRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Получение параметра комнаты
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost(nameof(GetRoomParameter), Name = nameof(GetRoomParameter))]
    public async Task<ActionResult> GetRoomParameter(GetRoomParameterQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
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
    [HttpPost(nameof(ClearHistoryRoom), Name = nameof(ClearHistoryRoom))]
    public async Task<ActionResult> ClearHistoryRoom(ClearHistoryRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Добавление пользователя в группу
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost(nameof(EnterRoom), Name = nameof(EnterRoom))]
    public async Task<ActionResult> EnterRoom(EnterRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Выход/удаление пользователя из группы
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost(nameof(ExitRoom), Name = nameof(ExitRoom))]
    public async Task<ActionResult> ExitRoom(ExitRoomCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}