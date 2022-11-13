namespace FiveLetters.BL.Models;

public sealed record LetterState
{
    public char Ch { get; init; }
    public LetterStatus Status { get; set; }

    public LetterState(char ch, LetterStatus status)
    {
        Ch = ch;
        Status = status;
    }
}
