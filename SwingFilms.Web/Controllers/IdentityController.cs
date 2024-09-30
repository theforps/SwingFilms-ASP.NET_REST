using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Services.Features.Identity.Commands;
using SwingFilms.Services.Features.Identity.Queries;

namespace SwingFilms.Controllers;

[Route("[controller]")]
public class IdentityController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public IdentityController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Вход пользователя в систему
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>JWT токен</returns>
    [HttpPost(nameof(Login), Name = nameof(Login))]
    public async Task<ActionResult> Login(LoginSystemCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Регистрация пользователя в системе
    /// </summary>
    /// <param name="command">Команда</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>JWT токен</returns>
    [HttpPost(nameof(Registration), Name = nameof(Registration))]
    public async Task<ActionResult> Registration(RegistrationSystemCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
    
    /// <summary>
    /// Получение информации о пользователе
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Информация о пользователе</returns>
    [HttpGet(nameof(GetUserInfo), Name = nameof(GetUserInfo))]
    public async Task<ActionResult> GetUserInfo(GetUserInfoQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        
        return Ok(result);
    }
}