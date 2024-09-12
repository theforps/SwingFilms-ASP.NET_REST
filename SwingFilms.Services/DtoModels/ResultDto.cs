namespace SwingFilms.Services.DtoModels;

/// <summary>
/// Ответ проверки соединения
/// </summary>
public sealed record ResultDto<T>
{
    public ResultDto(T? resultData, string? message = null, bool success = true)
    {
        ResultData = resultData;
        Message = message;
        Success = success;
    }
    
    /// <summary>
    /// Статус ответа
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Содержимое ответа
    /// </summary>
    public T? ResultData { get; init; }
    
    /// <summary>
    /// Соообщение
    /// </summary>
    public string? Message { get; init; }
}