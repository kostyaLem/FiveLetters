namespace FiveLetters.BL.Services;

internal interface IWordChecker
{
    bool CheckWord(string word, int lettersCount);
}
