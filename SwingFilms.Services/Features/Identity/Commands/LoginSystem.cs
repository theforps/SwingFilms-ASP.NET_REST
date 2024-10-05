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

/// <summary>
/// Вход пользователя в систему
/// </summary>
public sealed record LoginSystemCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// DTO для входа в систему
    /// </summary>
    [Required]
    [FromBody]
    public LoginUserDto Body { get; set; }
}

public class LoginSystemCommandValidator : AbstractValidator<LoginSystemCommand>
{
    public LoginSystemCommandValidator(IStringLocalizer<LoginSystemCommandValidator> localizer)
    {
        RuleFor(x => x.Body.TelegramId)
            .NotEqual(0)
            .WithMessage(localizer["TELEGRAM_ID_IS_EMPTY"])
            .When(x => string.IsNullOrEmpty(x.Body.Login) && string.IsNullOrEmpty(x.Body.Password));
        
        RuleFor(x => x.Body.Login)
            .NotEmpty()
            .WithMessage(localizer["LOGIN_IS_EMPTY"])
            .When(x => x.Body.TelegramId == 0);
        
        RuleFor(x => x.Body.Password)
            .NotEmpty()
            .WithMessage(localizer["PASSWORD_IS_EMPTY"])
            .When(x => x.Body.TelegramId == 0);
    }
}

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

        User? user;
        var isTelegramUser = request.Body.TelegramId != 0;
        
        if (isTelegramUser)
            _memoryCache.TryGetValue(request.Body.TelegramId, out user);
        else
            _memoryCache.TryGetValue(request.Body.Login, out user);
        
        if (user == null)
        {
            if (isTelegramUser)
            {
                user = await _userRepository.GetByTelegramId(request.Body.TelegramId, cancellationToken);

                if (user == null)
                {
                    user = new User
                    {
                        TelegramId = request.Body.TelegramId
                    };
                    await _userRepository.Add(user, cancellationToken);
                }
            }
            else
            {
                user = await _userRepository.GetByLogin(request.Body.Login, cancellationToken);
                
                if (user == null)
                    return new ResultDto<string>(null, _localizer["LOGIN_IS_WRONG", request.Body.Login], false);
                
                if (!_identityService.VerifyPassword(request.Body.Password, user.Password!))
                            return new ResultDto<string>(null, _localizer["PASSWORD_IS_WRONG", request.Body.Password], false);
            }
        }
        
        if (isTelegramUser)
            _memoryCache.Set(request.Body.TelegramId, user, TimeSpan.FromMinutes(1));
        else
            _memoryCache.Set(request.Body.Login, user, TimeSpan.FromMinutes(1));
        
        _memoryCache.Set(user.Id, user, TimeSpan.FromMinutes(15));
        
        var tokenJwt = _identityService.CreateTokenJwt(user.Role, user.Id);
        
        return new ResultDto<string>(tokenJwt);
    }
}