namespace FiveLetters.BL.Services;

/// <summary>
/// Класс для проверки букв английского алфавита
/// </summary>
internal class EngWordChecker : IWordChecker
{
    public bool CheckWord(string word, int lettersCount) 
        => word.All(ch => (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
        && word.Length == lettersCount;
}
