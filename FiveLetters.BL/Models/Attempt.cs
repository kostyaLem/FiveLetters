namespace FiveLetters.BL.Models;

public sealed record Attempt(bool IsGuessed, IReadOnlyList<LetterState> Letters);
