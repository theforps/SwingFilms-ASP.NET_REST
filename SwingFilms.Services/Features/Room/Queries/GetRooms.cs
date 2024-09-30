using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;
using SwingFilms.Services.Features.Room.DtoModels;

namespace SwingFilms.Services.Features.Room.Queries;

/// <summary>
/// Получение информации о комнатах
/// </summary>
public sealed record GetRoomsQuery : IRequest<ResultDto<SpaceRoomDto[]>>
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    [FromQuery]
    [Required]
    public Guid UserId { get; init; }
}

public class GetRoomsQueryValidator : AbstractValidator<GetRoomsQuery>
{
    public GetRoomsQueryValidator(IStringLocalizer<GetRoomsQueryValidator> localizer)
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage(localizer["USER_ID_IS_EMPTY"]);
    }
}

public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, ResultDto<SpaceRoomDto[]>>
{
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetRoomsQuery> _validator;
    
    public GetRoomsQueryHandler(
        ISpaceRoomRepository spaceRoomRepository, 
        IMapper mapper, 
        IValidator<GetRoomsQuery> validator)
    {
        _spaceRoomRepository = spaceRoomRepository;
        _mapper = mapper;
        _validator = validator;
    }
    
    public async Task<ResultDto<SpaceRoomDto[]>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<SpaceRoomDto[]>(null, string.Join(", ", validationResult.Errors), false);
        
        var spaceRooms =  await _spaceRoomRepository.GetAll(request.UserId, cancellationToken);
        
        var spaceRoomsDto = _mapper.Map<SpaceRoomDto[]>(spaceRooms);
        
        return new ResultDto<SpaceRoomDto[]>(spaceRoomsDto);
    }
}