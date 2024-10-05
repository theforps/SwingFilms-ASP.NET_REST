using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Identity.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Identity.Commands;

/// <summary>
/// Регистрация пользователя в системе
/// </summary>
public sealed record RegistrationSystemCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// DTO для регистрации в системе
    /// </summary>
    [Required]
    [FromForm]
    public RegistrationUserDto Body { get; init; }
}

public class RegistrationSystemCommandValidator : AbstractValidator<RegistrationSystemCommand>
{
    public RegistrationSystemCommandValidator(IStringLocalizer<RegistrationSystemCommandValidator> localizer)
    {
        RuleFor(x => x.Body)
            .NotNull();
        
        RuleFor(x => x.Body.Login)
            .NotEmpty()
            .WithMessage(localizer["LOGIN_IS_EMPTY"]);
        
        RuleFor(x => x.Body.Password)
            .NotEmpty()
            .WithMessage(localizer["PASSWORD_IS_EMPTY"]);
                
        RuleFor(x => x.Body.Role)
            .NotEmpty()
            .WithMessage(localizer["ROLE_IS_EMPTY"]);
    }
}

public class RegistrationSystemCommandHandler : IRequestHandler<RegistrationSystemCommand, ResultDto<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<RegistrationSystemCommandHandler> _localizer;
    private readonly IValidator<RegistrationSystemCommand> _validator;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public RegistrationSystemCommandHandler(
        IUserRepository userRepository, 
        IValidator<RegistrationSystemCommand> validator, 
        IStringLocalizer<RegistrationSystemCommandHandler> localizer, 
        IIdentityService identityService, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _validator = validator;
        _localizer = localizer;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<ResultDto<string>> Handle(RegistrationSystemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);

        var existingUser = await _userRepository.GetByLogin(request.Body.Login, cancellationToken);

        if (existingUser != null)
        {
            return new ResultDto<string>(null, _localizer["USER_ALREADY_EXISTS", request.Body.Login], false);
        }

        var user = _mapper.Map<User>(request.Body);
        
        user.Password = _identityService.HashPassword(request.Body.Password);
        
        if (request.Body.Image != null)
            user.Image = await _identityService.ConvertFormFileToByteArray(request.Body.Image, cancellationToken);

        await _userRepository.Add(user, cancellationToken);
        
        return new ResultDto<string>(null);
    }
}