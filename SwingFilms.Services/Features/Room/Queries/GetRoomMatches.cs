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

public class GetRoomMatchesQuery : IRequest<ResultDto<HistoryDto[]>>
{
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
            .WithMessage(localizer["ROOM_IS_IS_EMPTY"]);
    }
}

public class GetRoomMatchesQueryHandler : IRequestHandler<GetRoomMatchesQuery, ResultDto<HistoryDto[]>>
{
    private readonly IValidator<GetRoomMatchesQuery> _validator;
    private readonly IMemoryCache _memoryCache;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IHistoryRoomRepository _historyRoomRepository;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<GetRoomMatchesQueryHandler> _localizer;

    public GetRoomMatchesQueryHandler(
        IValidator<GetRoomMatchesQuery> validator,
        IStringLocalizer<GetRoomMatchesQueryHandler> localizer,
        ISpaceRoomRepository spaceRoomRepository,
        IMemoryCache memoryCache,
        IMapper mapper, 
        IHistoryRoomRepository historyRoomRepository)
    {
        _validator = validator;
        _localizer = localizer;
        _spaceRoomRepository = spaceRoomRepository;
        _memoryCache = memoryCache;
        _mapper = mapper;
        _historyRoomRepository = historyRoomRepository;
    }

    public async Task<ResultDto<HistoryDto[]>> Handle(GetRoomMatchesQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<HistoryDto[]>(null, string.Join(", ", validationResult.Errors), false);
        
        var spaceRoom = _memoryCache.Get<SpaceRoom>(request.RoomId) ?? await _spaceRoomRepository.GetById(request.RoomId, cancellationToken);
        
        if (spaceRoom != null)
            _memoryCache.Set(request.RoomId, spaceRoom);
        else
            return new ResultDto<HistoryDto[]>(null, _localizer["SPACE_ROOM_NOT_FOUND"], false);
        
        var roomMatches = await _historyRoomRepository.GetRoomMatches(spaceRoom.Id, cancellationToken);

        var roomMatchesDto = _mapper.Map<HistoryDto[]>(roomMatches);

        return new ResultDto<HistoryDto[]>(roomMatchesDto);
    }
}