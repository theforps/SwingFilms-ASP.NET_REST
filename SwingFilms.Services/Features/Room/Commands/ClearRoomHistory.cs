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

public class ClearRoomHistoryCommand : IRequest<ResultDto<string>>
{
    [FromQuery]
    [Required]
    public Guid RoomId { get; init; }
}

public class ClearRoomHistoryCommandValidator : AbstractValidator<ClearRoomHistoryCommand>
{
    public ClearRoomHistoryCommandValidator(IStringLocalizer<ClearRoomHistoryCommandValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class ClearRoomHistoryCommandHandler : IRequestHandler<ClearRoomHistoryCommand, ResultDto<string>>
{
    private readonly IValidator<ClearRoomHistoryCommand> _validator;
    private readonly IMemoryCache _memoryCache;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IHistoryRoomRepository _historyRoomRepository;
    private readonly IStringLocalizer<ClearRoomHistoryCommandHandler> _localizer;

    public ClearRoomHistoryCommandHandler(
        IValidator<ClearRoomHistoryCommand> validator,
        IStringLocalizer<ClearRoomHistoryCommandHandler> localizer,
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

    public async Task<ResultDto<string>> Handle(ClearRoomHistoryCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);

        var spaceRoom = _memoryCache.Get<SpaceRoom>(request.RoomId) ?? await _spaceRoomRepository.GetById(request.RoomId, cancellationToken);
        
        if (spaceRoom != null)
            _memoryCache.Set(request.RoomId, spaceRoom);
        else
            return new ResultDto<string>(null, _localizer["ROOM_NOT_FOUND"], false);

        await _historyRoomRepository.ClearRoomHistory(request.RoomId, cancellationToken);

        return new ResultDto<string>(null);
    }
}