﻿namespace SwingFilms.Services.Features.Room.DtoModels;

public sealed record HistoryDto
{
    public Guid[] AmateurUsersIds { get; init; }
    
    public int FilmId { get; init; }
}