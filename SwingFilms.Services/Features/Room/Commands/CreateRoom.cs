using System.Security.Claims;
using FluentValidation;
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
    private readonly ITelegramUserRepository _telegramUserRepository;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly ISpaceRoomService _roomService;
    private readonly IStringLocalizer<CreateRoomCommandHandler> _localizer;
    
    public CreateRoomCommandHandler(
        IHttpContextAccessor httpContextAccessor, 
        IStringLocalizer<CreateRoomCommandHandler> localizer, 
        ISpaceRoomService roomService, 
        ISpaceRoomRepository spaceRoomRepository, 
        ITelegramUserRepository telegramUserRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _roomService = roomService;
        _spaceRoomRepository = spaceRoomRepository;
        _telegramUserRepository = telegramUserRepository;
    }
    
    public async Task<ResultDto<string>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var userIdString = _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Sid)!.Value;

        var spaceRoom = new SpaceRoom();

        if (int.TryParse(userIdString, out var userIdGuid))
        {
            var user = await _telegramUserRepository.GetUserByTelegramId(userIdGuid, cancellationToken);
            
            spaceRoom.Admin = user;
            spaceRoom.Members.Add(user);
        }
        else
        {
            return new ResultDto<string>(null, _localizer["USER_NOT_FOUND"], false);
        }

        spaceRoom.Code = _roomService.GenerateSpaceRoomCode();

        await _spaceRoomRepository.AddSpaceRoom(spaceRoom, cancellationToken);
        
        return new ResultDto<string>(null);
    }
}