namespace FiveLetters.BL.Services;

public enum LetterState
{
    NotGuessed,
    Wrong,
    Nearly,
    Guessed
}

public sealed record Attempt(bool IsGuessed, IReadOnlyList<(char, LetterState)> Letters);

public sealed class GameProcessor
{
    private readonly string _word;

    public GameProcessor(string word)
    {
        _word = word;
    }

    public Attempt TryGuess(string word)
    {
        var states = new List<(char Ch, LetterState State)>();

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
                    .Count(x => x == (word[i], LetterState.Guessed)
                             || x == (word[i], LetterState.Nearly));

                var allCount = word.Count(x => x == word[i]);

                if (guessedCount < allCount)
                {
                    states.Add((_word[i], LetterState.Nearly));
                    continue;
                }
            }

            states.Add((_word[i], LetterState.Wrong));
        }

        return new(states.All(x => x.State == LetterState.Guessed), states);
    }
}