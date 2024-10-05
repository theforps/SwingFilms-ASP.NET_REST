using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Identity.DtoModels;
using SwingFilms.Services.Services.Interfaces;

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
    private readonly IMapper _mapper;
    private readonly IMemoryService _memoryService;
    private readonly IValidator<GetUserInfoQuery> _validator;
    private readonly IStringLocalizer<GetUserInfoQueryHandler> _localizer;
    
    public GetUserInfoQueryHandler(
        IValidator<GetUserInfoQuery> validator, 
        IStringLocalizer<GetUserInfoQueryHandler> localizer,
        IMapper mapper,
        IMemoryService memoryService)
    {
        _validator = validator;
        _localizer = localizer;
        _mapper = mapper;
        _memoryService = memoryService;
    }

    public async Task<ResultDto<UserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ResultDto<UserInfoDto>(null, string.Join(", ", validationResult.Errors), false);

        var user = await _memoryService.GetUserById(request.UserId, cancellationToken);

        if (user == null)
            return new ResultDto<UserInfoDto>(null, _localizer["USER_WAS_NOT_FOUND", request.UserId], false);
        
        var userDto = _mapper.Map<UserInfoDto>(user);
        
        return new ResultDto<UserInfoDto>(userDto);
    }
}