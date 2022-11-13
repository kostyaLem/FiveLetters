using DevExpress.Mvvm;
using System;

namespace FiveLetters.UI.Models;

internal record Settings
{
    public Uri FilePath { get; init; }
    public int LettersCount { get; init; }
    public LangMode LangMode { get; init; }
}
