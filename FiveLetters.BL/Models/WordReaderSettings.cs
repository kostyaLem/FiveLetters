namespace FiveLetters.BL.Models;

public record WordReaderSettings
{
    public string FilePath { get; }
    public LangMode LangMode { get; }
    public int LettersCount { get; }

    public WordReaderSettings(string filePath, LangMode langMode, int lettersCount)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        if (lettersCount <= 0)
        {
            throw new ArgumentException(nameof(lettersCount));
        }

        FilePath = filePath;
        LangMode = langMode;
        LettersCount = lettersCount;
    }
}
