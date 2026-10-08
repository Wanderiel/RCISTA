namespace Domain.Models.NPMs;

/// <summary>
/// Статусы состояния
/// </summary>
public enum NpmStatus
{
    /// <summary> Рабочий </summary>
    Good = 0,
    /// <summary> Неработоспособный </summary>
    Broken = 1,
    /// <summary> Содержит некореектные данные (производитлеь, модель и т.д.) </summary>
    Invalid = -1
}
