using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Room.DtoModels;
using SwingFilms.Services.Services.Interfaces;

namespace SwingFilms.Services.Features.Room.Queries;

/// <summary>
/// Получение совпадений в комнате
/// </summary>
public sealed record GetRoomMatchesQuery : IRequest<ResultDto<HistoryDto[]>>
{
    /// <summary>
    /// Уникальный идентификатор комнаты
    /// </summary>
    [FromQuery]
    [Required]
    public Guid RoomId { get; init; }
}

public class GetRoomMatchesQueryValidator : AbstractValidator<GetRoomMatchesQuery>
{
    public GetRoomMatchesQueryValidator(IStringLocalizer<GetRoomMatchesQueryValidator> localizer)
    {
        RuleFor(x => x.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class GetRoomMatchesQueryHandler : IRequestHandler<GetRoomMatchesQuery, ResultDto<HistoryDto[]>>
{
    private readonly IValidator<GetRoomMatchesQuery> _validator;
    private readonly IHistoryRoomRepository _historyRoomRepository;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetRoomMatchesQueryHandler> _localizer;
    private readonly IMemoryService _memoryService;

    public GetRoomMatchesQueryHandler(
        IValidator<GetRoomMatchesQuery> validator,
        IStringLocalizer<GetRoomMatchesQueryHandler> localizer,
        IMapper mapper, 
        IHistoryRoomRepository historyRoomRepository, IMemoryService memoryService)
    {
        _validator = validator;
        _localizer = localizer;
        _mapper = mapper;
        _historyRoomRepository = historyRoomRepository;
        _memoryService = memoryService;
    }

    public async Task<ResultDto<HistoryDto[]>> Handle(GetRoomMatchesQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<HistoryDto[]>(null, string.Join(", ", validationResult.Errors), false);

        var spaceRoom = await _memoryService.GetSpaceRoomById(request.RoomId, cancellationToken);
        
        if (spaceRoom == null)
            return new ResultDto<HistoryDto[]>(null, _localizer["ROOM_WAS_NOT_FOUND", request.RoomId], false);
        
        var roomMatches = await _historyRoomRepository.GetRoomMatches(request.RoomId, cancellationToken);

        var roomMatchesDto = _mapper.Map<HistoryDto[]>(roomMatches);

        return new ResultDto<HistoryDto[]>(roomMatchesDto);
    }
}