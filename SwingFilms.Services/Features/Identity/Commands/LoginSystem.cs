using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Identity.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Identity.Commands;

// TODO доделать команду и подключить
/// <summary>
/// Вход пользователя в систему
/// </summary>
public class LoginSystemCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// DTO для входа в систему
    /// </summary>
    [Required]
    [FromBody]
    public LoginUserDto Body { get; set; }
}

/// <summary>
/// Валидация входных данных
/// </summary>
public class LoginSystemCommandValidator : AbstractValidator<LoginSystemCommand>
{
    public LoginSystemCommandValidator(IStringLocalizer<LoginSystemCommandValidator> localizer)
    { 
        RuleFor(x => x.Body.Login)
            .NotEmpty()
            .WithMessage(localizer["LOGIN_IS_EMPTY"]);
        
        RuleFor(x => x.Body.Password)
            .NotEmpty()
            .WithMessage(localizer["PASSWORD_IS_EMPTY"]);
    }
}

/// <summary>
/// Обработчик входа в систему
/// </summary>
public class LoginSystemCommandHandler : IRequestHandler<LoginSystemCommand, ResultDto<string>>
{
    private readonly IStringLocalizer<LoginSystemCommandHandler> _localizer;
    private readonly IIdentityService _identityService;
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IValidator<LoginSystemCommand> _validator;
    
    public LoginSystemCommandHandler(
        IStringLocalizer<LoginSystemCommandHandler> localizer, 
        IValidator<LoginSystemCommand> validator,
        IMemoryCache memoryCache,
        IUserRepository userRepository, 
        IIdentityService identityService)
    {
        _localizer = localizer;
        _validator = validator;
        _memoryCache = memoryCache;
        _userRepository = userRepository;
        _identityService = identityService;
    }
    
    public async Task<ResultDto<string>> Handle(LoginSystemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);

        _memoryCache.TryGetValue(request.Body.Login, out User? user);
        
        if (user == null)
        {
            user = await _userRepository.GetByLogin(request.Body.Login, cancellationToken);
            
            if (user == null)
                return new ResultDto<string>(null, _localizer["LOGIN_IS_WRONG"], false);
        
            _memoryCache.Set(request.Body.Login, user, TimeSpan.FromMinutes(5));
        }
        
        if (!_identityService.VerifyPassword(request.Body.Password, user.Password))
            return new ResultDto<string>(null, _localizer["PASSWORD_IS_WRONG"], false);

        var tokenJwt = _identityService.CreateTokenJwt(user.Role, user.Id, default);
        
        return new ResultDto<string>(tokenJwt);
    }
}