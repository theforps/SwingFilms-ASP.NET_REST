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

namespace SwingFilms.Services.Features.Room.Queries;

/// <summary>
/// Получение параметров комнаты
/// </summary>
public sealed record GetRoomParameterQuery : IRequest<ResultDto<ParameterDto>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [Required]
    [FromQuery]
    public Guid RoomId { get; init; }
}

public class GetRoomParameterQueryValidator : AbstractValidator<GetRoomParameterQuery>
{
    public GetRoomParameterQueryValidator(IStringLocalizer<GetRoomParameterQueryValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class GetRoomParameterQueryHandler : IRequestHandler<GetRoomParameterQuery, ResultDto<ParameterDto>>
{
    private readonly IMemoryCache _memoryCache;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetRoomParameterQuery> _validator;
    private readonly IStringLocalizer<GetRoomParameterQueryHandler> _localizer;
    
    public GetRoomParameterQueryHandler(
        IMemoryCache memoryCache, 
        ISpaceRoomRepository spaceRoomRepository, 
        IMapper mapper, 
        IStringLocalizer<GetRoomParameterQueryHandler> localizer, 
        IValidator<GetRoomParameterQuery> validator)
    {
        _memoryCache = memoryCache;
        _spaceRoomRepository = spaceRoomRepository;
        _mapper = mapper;
        _localizer = localizer;
        _validator = validator;
    }
    
    public async Task<ResultDto<ParameterDto>> Handle(GetRoomParameterQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<ParameterDto>(null, string.Join(", ", validationResult.Errors), false);
        
        var spaceRoom = _memoryCache.Get<SpaceRoom>(request.RoomId) 
                        ?? await _spaceRoomRepository.GetById(request.RoomId, cancellationToken);
        
        if (spaceRoom == null)
            return new ResultDto<ParameterDto>(null, _localizer["SPACE_ROOM_NOT_FOUND"], false);
        
        var roomParameter = await _spaceRoomRepository.GetParameter(request.RoomId, cancellationToken);
        
        var roomParameterDto = _mapper.Map<ParameterDto>(roomParameter);
        
        return new ResultDto<ParameterDto>(roomParameterDto);
    }
}