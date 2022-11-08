namespace FiveLetters.BL.Services;

internal class EngWordChecker : IWordChecker
{
    public bool CheckWord(string word, int lettersCount) 
        => word.All(ch => (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z'))
        && word.Length == lettersCount;
}
