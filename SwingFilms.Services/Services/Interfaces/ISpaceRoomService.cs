namespace SwingFilms.Services.Services.Interfaces;

/// <summary>
/// Сервис комнаты
/// </summary>
public interface ISpaceRoomService
{
    /// <summary>
    /// Генерация уникального кода комнаты
    /// </summary>
    /// <returns>Код комнаты</returns>
    string GenerateSpaceRoomCode();
}