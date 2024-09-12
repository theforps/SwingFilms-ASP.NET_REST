using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Enums;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Identity.Commands;

/// <summary>
/// Команда проверки существования телеграм пользователя в системе
/// </summary>
public class SocialLoginSystemCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// Telegram Id для входа в систему
    /// </summary>
    [FromQuery]
    [Required]
    public int TelegramUserId { get; set; }
}

/// <summary>
/// Валидация входных данных
/// </summary>
public class SocialLoginSystemCommandValidator : AbstractValidator<SocialLoginSystemCommand>
{
    public SocialLoginSystemCommandValidator(IStringLocalizer<SocialLoginSystemCommandValidator> localizer)
    {
        RuleFor(x => x.TelegramUserId)
            .NotEmpty()
            .WithMessage(localizer["TELEGRAM_USER_ID_IS_EMPTY"]);
    }
}

/// <summary>
/// Обработчик входа в систему
/// </summary>
public class SocialLoginSystemCommandHandler : IRequestHandler<SocialLoginSystemCommand, ResultDto<string>>
{
    private readonly IValidator<SocialLoginSystemCommand> _validator;
    private readonly IMemoryCache _memoryCache;
    private readonly IIdentityService _identityService;
    private readonly ITelegramUserRepository _telegramUserRepository;
    
    public SocialLoginSystemCommandHandler(
        IValidator<SocialLoginSystemCommand> validator, 
        IMemoryCache memoryCache, 
        ITelegramUserRepository telegramUserRepository, 
        IIdentityService identityService)
    {
        _validator = validator;
        _memoryCache = memoryCache;
        _telegramUserRepository = telegramUserRepository;
        _identityService = identityService;
    }
    
    public async Task<ResultDto<string>> Handle(SocialLoginSystemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);

        _memoryCache.TryGetValue(request.TelegramUserId, out TelegramUser? telegramUser);
        
        if (telegramUser == null)
        {
            telegramUser = await _telegramUserRepository.GetUserByTelegramId(request.TelegramUserId, cancellationToken);

            if (telegramUser == null)
            {
                telegramUser = new TelegramUser
                {
                    Id = request.TelegramUserId
                };
                
                await _telegramUserRepository.AddTelegramUser(telegramUser, cancellationToken);
            }
        
            _memoryCache.Set(request.TelegramUserId, telegramUser, TimeSpan.FromMinutes(5));
        }

        var tokenJwt = _identityService.CreateTokenJwt(UserRole.User, default, telegramUser.Id);
        
        return new ResultDto<string>(tokenJwt);
    }
}
