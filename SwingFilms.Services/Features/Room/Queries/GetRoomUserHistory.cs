using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Room.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Room.Queries;

/// <summary>
/// Получение истории пользователя в комнате
/// </summary>
public sealed record GetRoomUserHistoryQuery : IRequest<ResultDto<HistoryDto[]>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [FromQuery]
    [Required]
    public Guid RoomId { get; init; }
}

public class GetRoomUserHistoryQueryValidator : AbstractValidator<GetRoomUserHistoryQuery>
{
    public GetRoomUserHistoryQueryValidator(IStringLocalizer<GetRoomUserHistoryQueryValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class GetRoomUserHistoryQueryHandler : IRequestHandler<GetRoomUserHistoryQuery, ResultDto<HistoryDto[]>>
{
    private readonly IValidator<GetRoomUserHistoryQuery> _validator;
    private readonly IMemoryService _memoryService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHistoryRoomRepository _historyRepository;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetRoomUserHistoryQueryHandler> _localizer;
    
    public GetRoomUserHistoryQueryHandler(
        IValidator<GetRoomUserHistoryQuery> validator, 
        IStringLocalizer<GetRoomUserHistoryQueryHandler> localizer, 
        IMemoryService memoryService, 
        IHttpContextAccessor httpContextAccessor, 
        IHistoryRoomRepository historyRepository, 
        IMapper mapper)
    {
        _validator = validator;
        _localizer = localizer;
        _memoryService = memoryService;
        _httpContextAccessor = httpContextAccessor;
        _historyRepository = historyRepository;
        _mapper = mapper;
    }
    
    public async Task<ResultDto<HistoryDto[]>> Handle(GetRoomUserHistoryQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<HistoryDto[]>(null, string.Join(", ", validationResult.Errors), false);

        var spaceRoom = await _memoryService.GetSpaceRoomById(request.RoomId, cancellationToken);
        
        if (spaceRoom == null)
            return new ResultDto<HistoryDto[]>(null, _localizer["ROOM_WAS_NOT_FOUND", request.RoomId], false);
        
        var user = await _memoryService.GetCurrentUser(_httpContextAccessor, cancellationToken);
        
        var histories = await _historyRepository.GetUserHistoryInRoom(user.Id, request.RoomId, cancellationToken);
        
        var historiesDto = _mapper.Map<HistoryDto[]>(histories);
        
        return new ResultDto<HistoryDto[]>(historiesDto);
    }
}