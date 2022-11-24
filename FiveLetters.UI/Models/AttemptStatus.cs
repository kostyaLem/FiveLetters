namespace FiveLetters.UI.Models;

/// <summary>
/// Результат нажатия на кнопку Enter
/// </summary>
internal enum AttemptStatus
{
    // Попытки кончились
    Lose,
    // Не угадали. Повторите попытку
    CanRepeat,
    // Слово угадано
    Win
}
