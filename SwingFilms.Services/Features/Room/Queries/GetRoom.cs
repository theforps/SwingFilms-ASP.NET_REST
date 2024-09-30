using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Room.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Room.Queries;

/// <summary>
/// Получение информации о комнате
/// </summary>
public sealed record GetRoomQuery : IRequest<ResultDto<SpaceRoomDto>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [FromQuery]
    [Required]
    public Guid RoomId { get; init; }
}

public class GetRoomQueryValidator : AbstractValidator<GetRoomQuery>
{
    public GetRoomQueryValidator(IStringLocalizer<GetRoomQueryValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, ResultDto<SpaceRoomDto>>
{
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetRoomQueryHandler> _localizer;
    private readonly IValidator<GetRoomQuery> _validator;
    private readonly IMemoryService _memoryService;
    
    public GetRoomQueryHandler(
        IMapper mapper, 
        IStringLocalizer<GetRoomQueryHandler> localizer, 
        IValidator<GetRoomQuery> validator, 
        IMemoryService memoryService)
    {
        _mapper = mapper;
        _localizer = localizer;
        _validator = validator;
        _memoryService = memoryService;
    }
    
    public async Task<ResultDto<SpaceRoomDto>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<SpaceRoomDto>(null, string.Join(", ", validationResult.Errors), false);
        
        var spaceRoom = await _memoryService.GetSpaceRoomById(request.RoomId, cancellationToken);
        
        if (spaceRoom == null)
            return new ResultDto<SpaceRoomDto>(null, _localizer["SPACE_ROOM_NOT_FOUND"], false);
        
        var spaceRoomDto = _mapper.Map<SpaceRoomDto>(spaceRoom);
        
        return new ResultDto<SpaceRoomDto>(spaceRoomDto);
    }
}