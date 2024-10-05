using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;

namespace SwingFilms.Services.Features.Room.Commands;

/// <summary>
/// Очистка истории комнаты
/// </summary>
public sealed record ClearHistoryRoomCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [FromQuery]
    [Required]
    public Guid RoomId { get; init; }
}

public class ClearHistoryRoomCommandValidator : AbstractValidator<ClearHistoryRoomCommand>
{
    public ClearHistoryRoomCommandValidator(IStringLocalizer<ClearHistoryRoomCommandValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class ClearHistoryRoomCommandHandler : IRequestHandler<ClearHistoryRoomCommand, ResultDto<string>>
{
    private readonly IValidator<ClearHistoryRoomCommand> _validator;
    private readonly IMemoryCache _memoryCache;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IHistoryRoomRepository _historyRoomRepository;
    private readonly IStringLocalizer<ClearHistoryRoomCommandHandler> _localizer;

    public ClearHistoryRoomCommandHandler(
        IValidator<ClearHistoryRoomCommand> validator,
        IStringLocalizer<ClearHistoryRoomCommandHandler> localizer,
        ISpaceRoomRepository spaceRoomRepository,
        IMemoryCache memoryCache,
        IHistoryRoomRepository historyRoomRepository)
    {
        _validator = validator;
        _localizer = localizer;
        _spaceRoomRepository = spaceRoomRepository;
        _memoryCache = memoryCache;
        _historyRoomRepository = historyRoomRepository;
    }

    public async Task<ResultDto<string>> Handle(ClearHistoryRoomCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);

        var spaceRoom = _memoryCache.Get<SpaceRoom>(request.RoomId) ?? await _spaceRoomRepository.GetById(request.RoomId, cancellationToken);
        
        if (spaceRoom != null)
            _memoryCache.Set(request.RoomId, spaceRoom);
        else
            return new ResultDto<string>(null, _localizer["ROOM_WAS_NOT_FOUND"], false);

        await _historyRoomRepository.ClearRoomHistory(request.RoomId, cancellationToken);

        return new ResultDto<string>(null);
    }
}