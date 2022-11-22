namespace FiveLetters.BL.Services;

/// <summary>
/// Класс для проверки букв русского алфавита
/// </summary>
internal class RusWordChecker : IWordChecker
{
    public bool CheckWord(string word, int lettersCount)
        => word.All(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я'))
        && word.Length == lettersCount;
}