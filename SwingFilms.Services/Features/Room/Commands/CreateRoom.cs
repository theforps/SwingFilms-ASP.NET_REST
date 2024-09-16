using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Room.Commands;

public class CreateRoomCommand : IRequest<ResultDto<string>>;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ResultDto<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly ISpaceRoomService _roomService;
    private readonly IMemoryService _memoryService;
    private readonly IStringLocalizer<CreateRoomCommandHandler> _localizer;
    
    public CreateRoomCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IStringLocalizer<CreateRoomCommandHandler> localizer, 
        ISpaceRoomService roomService, 
        ISpaceRoomRepository spaceRoomRepository, 
        IMemoryService memoryService)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _roomService = roomService;
        _spaceRoomRepository = spaceRoomRepository;
        _memoryService = memoryService;
    }
    
    public async Task<ResultDto<string>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var spaceRoom = new SpaceRoom();
        User? user = await _memoryService.GetUserById(_httpContextAccessor, cancellationToken);

        if (user != null)
        {
            spaceRoom.Id = Guid.NewGuid(); 
            spaceRoom.Code = _roomService.GenerateSpaceRoomCode();
            spaceRoom.Admin = user;
            spaceRoom.Members.Add(user);
        }
        else
        {
            return new ResultDto<string>(null, _localizer["USER_NOT_FOUND"], false);
        }

        await _spaceRoomRepository.Add(spaceRoom, cancellationToken);
        
        return new ResultDto<string>(null);
    }
}