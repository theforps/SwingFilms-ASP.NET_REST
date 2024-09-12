﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Services.Features.Identity.Commands;

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
    [ApiExplorerSettings(IgnoreApi = true)]
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
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost(nameof(Registration), Name = nameof(Registration))]
    public async Task<ActionResult> Registration(RegistrationSystemCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
        
    /// <summary>
    /// Вход пользователя в систему по Telegram Id
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>JWT токен</returns>
    [HttpPost(nameof(LoginByTelegram), Name = nameof(LoginByTelegram))]
    public async Task<ActionResult> LoginByTelegram(SocialLoginSystemCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }
}