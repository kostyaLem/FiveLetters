namespace FiveLetters.BL.Services;

/// <summary>
/// Интерфейс проверки слов
/// </summary>
internal interface IWordChecker
{
    bool CheckWord(string word, int lettersCount);
}
