using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Identity.Queries;

namespace SwingFilms.Services.Features.Room.Commands;

/// <summary>
/// Удаление комнаты
/// </summary>
public sealed record DeleteRoomCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [Required]
    [FromQuery]
    public Guid RoomId { get; init; }
}

public class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator(IStringLocalizer<DeleteRoomCommandValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ResultDto<string>>
{
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IValidator<DeleteRoomCommand> _validator;
    private readonly IStringLocalizer<DeleteRoomCommandHandler> _localizer;
    
    public DeleteRoomCommandHandler(
        ISpaceRoomRepository spaceRoomRepository, 
        IMemoryCache memoryCache, 
        IValidator<DeleteRoomCommand> validator, 
        IStringLocalizer<DeleteRoomCommandHandler> localizer)
    {
        _spaceRoomRepository = spaceRoomRepository;
        _memoryCache = memoryCache;
        _validator = validator;
        _localizer = localizer;
    }

    public async Task<ResultDto<string>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);

        var room = await _spaceRoomRepository.GetById(request.RoomId, cancellationToken);

        if (room == null)
            return new ResultDto<string>(null, _localizer["ROOM_WAS_NOT_FOUND", request.RoomId], false);
        
        await _spaceRoomRepository.Delete(request.RoomId, cancellationToken);

        _memoryCache.Remove(request.RoomId);
        
        return new ResultDto<string>(null);
    }
}
