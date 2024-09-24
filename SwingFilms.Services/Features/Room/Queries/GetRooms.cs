using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Room.DtoModels;

namespace SwingFilms.Services.Features.Room.Queries;

public class GetRoomsQuery : IRequest<ResultDto<SpaceRoomDto[]>>
{
    [FromQuery]
    [Required]
    public Guid UserId { get; init; }
}

public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, ResultDto<SpaceRoomDto[]>>
{
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IMapper _mapper;
    
    public GetRoomsQueryHandler(
        ISpaceRoomRepository spaceRoomRepository, 
        IMapper mapper)
    {
        _spaceRoomRepository = spaceRoomRepository;
        _mapper = mapper;
    }
    
    public async Task<ResultDto<SpaceRoomDto[]>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var spaceRooms =  await _spaceRoomRepository.GetAll(request.UserId, cancellationToken);
        
        var spaceRoomsDto = _mapper.Map<SpaceRoomDto[]>(spaceRooms);
        
        return new ResultDto<SpaceRoomDto[]>(spaceRoomsDto);
    }
}