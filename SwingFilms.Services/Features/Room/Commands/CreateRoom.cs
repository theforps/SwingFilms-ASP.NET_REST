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
    private readonly IUserRepository _userRepository;
    private readonly IStringLocalizer<CreateRoomCommandHandler> _localizer;
    
    public CreateRoomCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IStringLocalizer<CreateRoomCommandHandler> localizer, 
        ISpaceRoomService roomService, 
        ISpaceRoomRepository spaceRoomRepository, 
        IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _roomService = roomService;
        _spaceRoomRepository = spaceRoomRepository;
        _userRepository = userRepository;
    }
    
    public async Task<ResultDto<string>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var userIdString = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!.Value;

        var spaceRoom = new SpaceRoom();

        if (int.TryParse(userIdString, out var intUserId))
        {
            var user = await _userRepository.GetByTelegramId(intUserId, cancellationToken);
            
            spaceRoom.Admin = user;
            spaceRoom.Members.Add(user);
            spaceRoom.Code = _roomService.GenerateSpaceRoomCode();
        }
        else if (Guid.TryParse(userIdString, out var intUserGuid))
        {
            var user = await _userRepository.GetById(intUserGuid, cancellationToken);

            spaceRoom.Admin = user;
            spaceRoom.Members.Add(user);
            spaceRoom.Code = _roomService.GenerateSpaceRoomCode();
        }
        else
        {
            return new ResultDto<string>(null, _localizer["USER_NOT_FOUND"], false);
        }

        await _spaceRoomRepository.Add(spaceRoom, cancellationToken);
        
        return new ResultDto<string>(null);
    }
}