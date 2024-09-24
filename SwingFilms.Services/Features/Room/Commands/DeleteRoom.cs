using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Infrastructure.Repository.Interfaces;
using SwingFilms.Services.DtoModels;

namespace SwingFilms.Services.Features.Room.Commands;

public class DeleteRoomCommand : IRequest<ResultDto<string>>
{
    [Required]
    [FromQuery]
    public Guid SpaceRoomId { get; set; }
}

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ResultDto<string>>
{
    private readonly ISpaceRoomRepository _spaceRoomRepository;
    
    public DeleteRoomCommandHandler(ISpaceRoomRepository spaceRoomRepository)
    {
        _spaceRoomRepository = spaceRoomRepository;
    }


    public async Task<ResultDto<string>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        await _spaceRoomRepository.Delete(request.SpaceRoomId, cancellationToken);

        return new ResultDto<string>(null);
    }
}
