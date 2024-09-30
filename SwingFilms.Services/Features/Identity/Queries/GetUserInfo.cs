using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Identity.DtoModels;

namespace SwingFilms.Services.Features.Identity.Queries;

/// <summary>
/// Получение информации о пользователе
/// </summary>
public sealed record GetUserInfoQuery : IRequest<ResultDto<UserInfoDto>>
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    [FromQuery]
    [Required]
    public Guid UserId { get; init; }
}

public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoQueryValidator(IStringLocalizer<GetUserInfoQueryValidator> localizer)
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(localizer["USER_ID_IS_EMPTY"]);
    }
}

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, ResultDto<UserInfoDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    private readonly IValidator<GetUserInfoQuery> _validator;
    private readonly IStringLocalizer<GetUserInfoQueryHandler> _localizer;
    
    public GetUserInfoQueryHandler(
        IValidator<GetUserInfoQuery> validator, 
        IStringLocalizer<GetUserInfoQueryHandler> localizer,
        IMapper mapper, 
        IUserRepository userRepository, 
        IMemoryCache memoryCache)
    {
        _validator = validator;
        _localizer = localizer;
        _mapper = mapper;
        _userRepository = userRepository;
        _memoryCache = memoryCache;
    }

    public async Task<ResultDto<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ResultDto<UserInfoDto>(null, string.Join(", ", validationResult.Errors), false);

        var user = _memoryCache.Get<User>(request.UserId)
            ?? await _userRepository.GetById(request.UserId, cancellationToken);

        if (user == null)
            return new ResultDto<UserInfoDto>(null, _localizer["USER_NOT_FOUND"]);

        _memoryCache.Set(request.UserId, user);
        
        var userDto = _mapper.Map<UserInfoDto>(user);
        
        return new ResultDto<UserInfoDto>(userDto);
    }
}