using FiveLetters.BL.Models;
using FiveLetters.BL.Services.Readers;
using static FiveLetters.BL.Services.GameProcessor;

namespace FiveLetters.BL.Services;

public enum LetterStatus
{
    NotGuessed,
    Wrong,
    Nearly,
    Guessed
}

public sealed record LetterState
{
    public char Ch { get; init; }
    public LetterStatus Status { get; set; }

    public LetterState(char ch, LetterStatus status)
    {
        Ch = ch;
        Status = status;
    }
}

public sealed record Attempt(bool IsGuessed, IReadOnlyList<LetterState> Letters);

public sealed class GameProcessor
{
    private IList<string> _newWords; // Возможно поменять на очередь
    private string _currentWord;

    private readonly WordReader _wordReader;

    public GameProcessor(WordReader wordReader)
    {
        _wordReader = wordReader;
        _newWords = new List<string>() { "РУЧКА" };
        NextWord();
    }

    public async Task ResetSettings(WordReaderSettings settings)
    {
        _newWords = (await _wordReader.ReadWords(settings)).ToList();
    }

    public async Task NextWord()
    {
        if (_newWords.Count == 0)
        {
            throw new Exception();
        }

        _currentWord = _newWords[0];
        _newWords.RemoveAt(0);
    }

    public Attempt TryGuess(string word)
    {
        var states = new List<LetterState>();

        for (int i = 0; i < word.Length; i++)
        {
            LetterState state = word[i] switch
            {
                _ when _currentWord[i] == word[i] => new(word[i], LetterStatus.Guessed),
                _ when _currentWord.Contains(word[i]) => new(word[i], LetterStatus.Nearly),
                _ => new(word[i], LetterStatus.Wrong)
            };

            states.Add(state);
        }

        for (int i = 0; i < word.Length; i++)
        {
            var guessed = states
                .Where(x => x.Ch == word[i] &&
                    (x.Status == LetterStatus.Nearly || x.Status == LetterStatus.Guessed))
                .ToList();

            var letterCount = _currentWord.Count(x => x == word[i]);

            for (int j = 0, count = 0; j < guessed.Count; j++)
            {
                if (count < letterCount)
                {
                    count++;
                }
                else
                {
                    guessed[j].Status = LetterStatus.Wrong;
                }
            }
        }

        return new(states.All(x => x.Status == LetterStatus.Guessed), states);
    }

}