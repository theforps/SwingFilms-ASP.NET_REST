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
using SwingFilms.Services.Features.Room.DtoModels;

namespace SwingFilms.Services.Features.Room.Commands;

/// <summary>
/// Изменение параметров комнаты
/// </summary>
public sealed record EditParameterRoomCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [Required]
    [FromQuery]
    public Guid RoomId { get; init; }
    
    /// <summary>
    /// DTO для изменения параметров комнаты
    /// </summary>
    [FromBody]
    [Required]
    public EditParameterDto Body { get; init; }
}

public class EditParameterRoomCommandValidator : AbstractValidator<EditParameterRoomCommand>
{
    public EditParameterRoomCommandValidator(IStringLocalizer<EditParameterRoomCommandValidator> localizer)
    {
        RuleFor(x => x.Body)
            .NotNull();

        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class EditParameterRoomCommandHandler : IRequestHandler<EditParameterRoomCommand, ResultDto<string>>
{
    private readonly IValidator<EditParameterRoomCommand> _validator;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IStringLocalizer<EditParameterRoomCommandHandler> _localizer;

    public EditParameterRoomCommandHandler(
        IValidator<EditParameterRoomCommand> validator,
        IStringLocalizer<EditParameterRoomCommandHandler> localizer,
        ISpaceRoomRepository spaceRoomRepository,
        IMemoryCache memoryCache, 
        IMapper mapper)
    {
        _validator = validator;
        _localizer = localizer;
        _spaceRoomRepository = spaceRoomRepository;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }
    
    public async Task<ResultDto<string>> Handle(EditParameterRoomCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);
        
        var spaceRoom = _memoryCache.Get<SpaceRoom>(request.RoomId) ?? await _spaceRoomRepository.GetById(request.RoomId, cancellationToken);
        
        if (spaceRoom != null)
            _memoryCache.Set(request.RoomId, spaceRoom);
        else
            return new ResultDto<string>(null, _localizer["ROOM_WAS_NOT_FOUND", request.RoomId], false);

        var editedParameter = _mapper.Map<Parameter>(request.Body);

        await _spaceRoomRepository.UpdateParameter(spaceRoom.Id, editedParameter, cancellationToken);

        return new ResultDto<string>(null);
    }
}