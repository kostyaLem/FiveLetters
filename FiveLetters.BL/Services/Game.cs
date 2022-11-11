using FiveLetters.BL.Models;
using FiveLetters.BL.Services.Readers;

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
    private IList<string> _newWords; // Возможно поменять на очередь
    private string _currentWord;

    private readonly WordReader _wordReader;

    public GameProcessor(WordReader wordReader)
    {
        _wordReader = wordReader;
    }

    public async Task ResetSettings(WordReaderSettings settings)
    {
        _newWords = (await _wordReader.ReadWords(settings)).ToList();
    }

    public async Task NextWord()
    {
        if (_newWords.Count == 1)
        {
            throw new Exception();
        }

        _newWords.RemoveAt(0);
        _currentWord = _newWords[0];
    }

    public Attempt TryGuess(string word)
    {
        var states = new List<(char Ch, LetterState State)>();

        for (int i = 0; i < word.Length; i++)
        {
            if (_currentWord[i] == word[i])
            {
                states.Add((word[i], LetterState.Guessed));
                continue;
            }

            if (_currentWord.Contains(word[i]))
            {
                var guessedCount = states
                    .Count(x => x == (word[i], LetterState.Guessed)
                             || x == (word[i], LetterState.Nearly));

                var allCount = word.Count(x => x == word[i]);

                if (guessedCount < allCount)
                {
                    states.Add((word[i], LetterState.Nearly));
                    continue;
                }
            }

            states.Add((word[i], LetterState.Wrong));
        }

        return new(states.All(x => x.State == LetterState.Guessed), states);
    }
}