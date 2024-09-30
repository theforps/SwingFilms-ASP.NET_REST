using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Models;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Room.Commands;

/// <summary>
/// Создание комнаты
/// </summary>
public sealed record CreateRoomCommand : IRequest<ResultDto<string>>;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ResultDto<string>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISpaceRoomService _roomService;
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
        var spaceRoom = new SpaceRoom();
        
        var userIdString = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!.Value;
        
        var user = await _userRepository.GetById(Guid.Parse(userIdString), cancellationToken);

        if (user != null)
        {
            spaceRoom.Code = _roomService.GenerateSpaceRoomCode();
            spaceRoom.Admin = user;
            spaceRoom.Members.Add(user);
            spaceRoom.Parameter = new Parameter();
        }
        else
        {
            return new ResultDto<string>(null, _localizer["USER_NOT_FOUND"], false);
        }

        await _spaceRoomRepository.Add(spaceRoom, cancellationToken);
        
        return new ResultDto<string>(null);
    }
}