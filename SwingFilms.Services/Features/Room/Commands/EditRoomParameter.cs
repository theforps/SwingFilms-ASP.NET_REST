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

namespace SwingFilms.Services.Features.Room.Commands;

public class EditRoomParameterCommand : IRequest<ResultDto<string>>
{
    [FromBody]
    [Required]
    public EditParameterDto Body { get; init; }
}

public class EditRoomParameterCommandValidator : AbstractValidator<EditRoomParameterCommand>
{
    public EditRoomParameterCommandValidator(IStringLocalizer<EditRoomParameterCommandValidator> localizer)
    {
        RuleFor(x => x.Body)
            .NotNull();

        RuleFor(x => x.Body.RoomId)
            .NotEmpty()
            .WithMessage(localizer["ROOM_ID_IS_EMPTY"]);
    }
}

public class EditRoomParameterCommandHandler : IRequestHandler<EditRoomParameterCommand, ResultDto<string>>
{
    private readonly IValidator<EditRoomParameterCommand> _validator;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    private readonly IStringLocalizer<EditRoomParameterCommandHandler> _localizer;

    public EditRoomParameterCommandHandler(
        IValidator<EditRoomParameterCommand> validator,
        IStringLocalizer<EditRoomParameterCommandHandler> localizer,
        ISpaceRoomRepository spaceRoomRepository,
        IMemoryCache memoryCache, 
        IMapper mapper)
    {
        _validator = validator;
        _localizer = localizer;
        _spaceRoomRepository = spaceRoomRepository;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }
    
    public async Task<ResultDto<string>> Handle(EditRoomParameterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ResultDto<string>(null, string.Join(", ", validationResult.Errors), false);
        
        var spaceRoom = _memoryCache.Get<SpaceRoom>(request.Body.RoomId) ?? await _spaceRoomRepository.GetById(request.Body.RoomId, cancellationToken);
        
        if (spaceRoom != null)
            _memoryCache.Set(request.Body.RoomId, spaceRoom);
        else
            return new ResultDto<string>(null, _localizer["SPACE_ROOM_NOT_FOUND"], false);

        var editedParameter = _mapper.Map<Parameter>(request.Body);

        await _spaceRoomRepository.UpdateParameter(spaceRoom.Id, editedParameter, cancellationToken);

        return new ResultDto<string>(null);
    }
}