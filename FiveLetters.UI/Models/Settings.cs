using System;

namespace FiveLetters.UI.Models;

internal record Settings
{
    public Uri FilePath { get; init; }
    public int WordLength { get; init; }
    public LangMode LangMode { get; init; }
}
