using FiveLetters.BL.Models;
using FiveLetters.UI.Models;
using System;

namespace FiveLetters.UI.Mappers;

internal static class CellStyleMapper
{
    public static CellStyle Map(this LetterStatus letterStatus)
        => letterStatus switch
        {
            LetterStatus.NotGuessed => CellStyle.Empty,
            LetterStatus.Wrong => CellStyle.Wrong,
            LetterStatus.Nearly => CellStyle.Nearly,
            LetterStatus.Guessed => CellStyle.Guessed,
            _ => throw new ArgumentException($"Unsupported LetterStatus value {letterStatus}.")
        };
}
