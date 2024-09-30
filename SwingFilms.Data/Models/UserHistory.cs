namespace SwingFilms.Infrastructure.Models;

/// <summary>
/// Отношение пользователя к истории
/// </summary>
public class UserHistory
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Уникальный идентификатор истории
    /// </summary>
    public Guid HistoryId { get; set; }
}