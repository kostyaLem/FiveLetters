namespace FiveLetters.BL.Models;

/// <summary>
/// Класс-состояние для буквы
/// </summary>
public sealed record LetterState
{
    // Введённый символ
    public char Ch { get; init; }
    // Статус символа
    public LetterStatus Status { get; set; }

    public LetterState(char ch, LetterStatus status)
    {
        Ch = ch;
        Status = status;
    }
}
