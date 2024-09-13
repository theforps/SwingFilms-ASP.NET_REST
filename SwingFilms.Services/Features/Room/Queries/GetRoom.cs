using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SwingFilms.Services.DtoModels;

namespace SwingFilms.Services.Features.Room.Queries;

public class GetRoomQuery : IRequest<ResultDto<string>>
{
    [FromQuery]
    [Required]
    public int SpaceRoomId { get; init; }
}

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, ResultDto<string>>
{
    public Task<ResultDto<string>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}