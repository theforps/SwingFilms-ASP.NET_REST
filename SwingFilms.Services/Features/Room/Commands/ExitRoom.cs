using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Room.Commands;

/// <summary>
/// Выход пользователя из комнаты
/// </summary>
public sealed record ExitRoomCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [Required]
    [FromQuery]
    public Guid RoomId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    [Required]
    [FromQuery]
    public Guid UserId { get; init; }
}

public class ExitRoomCommandValidator : AbstractValidator<ExitRoomCommand>
{
    public ExitRoomCommandValidator(IStringLocalizer<ExitRoomCommandValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
        
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(localizer["USER_ID_IS_EMPTY"]);
    }
}

public class ExitRoomCommandHandler : IRequestHandler<ExitRoomCommand, ResultDto<string>>
{
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IValidator<ExitRoomCommand> _validator;
    private readonly IStringLocalizer<ExitRoomCommandHandler> _localizer;
    private readonly IMemoryService _memoryService;
    private readonly IMemoryCache _memoryCache;
    
    public ExitRoomCommandHandler(
        ISpaceRoomRepository spaceRoomRepository, 
        IValidator<ExitRoomCommand> validator, 
        IStringLocalizer<ExitRoomCommandHandler> localizer, 
        IMemoryService memoryService, 
        IMemoryCache memoryCache)
    {
        _spaceRoomRepository = spaceRoomRepository;
        _validator = validator;
        _localizer = localizer;
        _memoryService = memoryService;
        _memoryCache = memoryCache;
    }
    
    public async Task<ResultDto<string>> Handle(ExitRoomCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);
        
        var user = await _memoryService.GetUserById(request.UserId, cancellationToken);

        if (user == null)
        {
            return new ResultDto<string>(null, _localizer["USER_WAS_NOT_FOUND", request.UserId], false);
        }
        
        var room = await _memoryService.GetSpaceRoomById(request.RoomId, cancellationToken);

        if (room == null)
        {
            return new ResultDto<string>(null, _localizer["ROOM_WAS_NOT_FOUND", request.RoomId], false);
        }
        
        await _spaceRoomRepository.RemoveMember(request.RoomId, request.UserId, cancellationToken);

        _memoryCache.Remove(request.RoomId);
        
        return new ResultDto<string>(null);
    }
}