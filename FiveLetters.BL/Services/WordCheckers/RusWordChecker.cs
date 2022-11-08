namespace FiveLetters.BL.Services;

internal class RusWordChecker : IWordChecker
{
    public bool CheckWord(string word, int lettersCount)
        => word.All(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я'))
        && word.Length == lettersCount;
}