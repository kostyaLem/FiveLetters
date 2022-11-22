namespace FiveLetters.BL.Models.Settings;

/// <summary>
/// Настройки игры
/// </summary>
public record WordReaderSettings
{
    // Путь до файла со словами
    public string FilePath { get; }
    // Режим игры и слов
    public LangMode LangMode { get; }
    // Длина слов
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
