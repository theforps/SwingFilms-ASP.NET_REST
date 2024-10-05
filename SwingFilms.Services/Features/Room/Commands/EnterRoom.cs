using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;

namespace SwingFilms.Services.Features.Room.Commands;

/// <summary>
/// Вход пользователя в комнату
/// </summary>
public sealed record EnterRoomCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// Код комнаты
    /// </summary>
    [Required]
    [FromQuery]
    public string RoomCode { get; init; }
}

public class EnterRoomCommandValidator : AbstractValidator<EnterRoomCommand>
{
    public EnterRoomCommandValidator(IStringLocalizer<EnterRoomCommandValidator> localizer)
    {
        RuleFor(x => x.RoomCode)
            .NotEmpty()
            .WithMessage(localizer["ROOM_CODE_IS_EMPTY"]);
    }
}

public class EnterRoomCommandHandler : IRequestHandler<EnterRoomCommand, ResultDto<string>>
{
    private readonly IValidator<EnterRoomCommand> _validator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IStringLocalizer<EnterRoomCommandHandler> _localizer;
    
    public EnterRoomCommandHandler(
        IValidator<EnterRoomCommand> validator, 
        IHttpContextAccessor httpContextAccessor, 
        ISpaceRoomRepository spaceRoomRepository, 
        IStringLocalizer<EnterRoomCommandHandler> localizer)
    {
        _validator = validator;
        _httpContextAccessor = httpContextAccessor;
        _spaceRoomRepository = spaceRoomRepository;
        _localizer = localizer;
    }
    
    public async Task<ResultDto<string>> Handle(EnterRoomCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);
        
        var userIdString = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!.Value;

        var room = await _spaceRoomRepository.GetByCode(request.RoomCode, cancellationToken);

        if (room == null)
            return new ResultDto<string>(null, _localizer["ROOM_WAS_NOT_FOUND", request.RoomCode], false);
        
        await _spaceRoomRepository.EnterUserToRoom(request.RoomCode, Guid.Parse(userIdString), cancellationToken);
        
        return new ResultDto<string>(null);
    }
}