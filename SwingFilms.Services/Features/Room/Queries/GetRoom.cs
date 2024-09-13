using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Room.DtoModels;

namespace SwingFilms.Services.Features.Room.Queries;

public class GetRoomQuery : IRequest<ResultDto<SpaceRoomDto>>
{
    [FromQuery]
    [Required]
    public Guid SpaceRoomId { get; init; }
}

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, ResultDto<SpaceRoomDto>>
{
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetRoomQueryHandler> _localizer;
    
    public GetRoomQueryHandler(
        ISpaceRoomRepository spaceRoomRepository, 
        IMemoryCache memoryCache, IMapper mapper, 
        IStringLocalizer<GetRoomQueryHandler> localizer)
    {
        _spaceRoomRepository = spaceRoomRepository;
        _memoryCache = memoryCache;
        _mapper = mapper;
        _localizer = localizer;
    }
    
    public async Task<ResultDto<SpaceRoomDto>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var spaceRoom = _memoryCache.Get<SpaceRoom>(request.SpaceRoomId) 
                   ?? await _spaceRoomRepository.GetById(request.SpaceRoomId, cancellationToken);
        
        if (spaceRoom == null)
            return new ResultDto<SpaceRoomDto>(null, _localizer["SPACE_ROOM_NOT_FOUND"], false);
        
        _memoryCache.Set(request.SpaceRoomId, spaceRoom, TimeSpan.FromMinutes(15));
        
        var spaceRoomDto = _mapper.Map<SpaceRoomDto>(spaceRoom);
        
        return new ResultDto<SpaceRoomDto>(spaceRoomDto);
    }
}