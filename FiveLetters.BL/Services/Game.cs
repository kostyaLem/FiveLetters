namespace FiveLetters.BL.Services;

internal enum LetterState
{
    NotGuessed,
    Wrong,
    Nearly,
    Guessed
}

internal sealed class Game
{
    private readonly string _word;

    public Game(string word)
    {
        _word = word;
    }

    public IReadOnlyList<(char, LetterState)> TryGuess(string word)
    {
        var states = new List<(char, LetterState)>();

        for (int i = 0; i < word.Length; i++)
        {
            if (_word[i] == word[i])
            {
                states.Add((_word[i], LetterState.Guessed));
                continue;
            }

            if (_word.Contains(word[i]))
            {
                var guessedCount = states
                    .Count(x => x == (word[i], LetterState.Guessed));

                var allCount = word.Count(x => x == word[i]);

                if (guessedCount < allCount)
                {
                    states.Add((_word[i], LetterState.Nearly));
                    continue;
                }
            }

            states.Add((_word[i], LetterState.Wrong));
        }

        return states;
    }
}