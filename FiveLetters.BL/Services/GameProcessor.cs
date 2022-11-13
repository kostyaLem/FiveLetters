using FiveLetters.BL.Models;
using FiveLetters.BL.Models.Settings;
using FiveLetters.BL.Services.Readers;

namespace FiveLetters.BL.Services;

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
                .OrderBy(x => x.Status)
                .ToList();

            var repeats = 0;
            var letterCount = _currentWord.Count(x => x == word[i]);

            guessed.ForEach(x =>
            {
                if (repeats < letterCount)
                {
                    repeats++;
                }
                else
                {
                    x.Status = LetterStatus.Wrong;
                }
            });
        }

        return new(states.All(x => x.Status == LetterStatus.Guessed), states);
    }
}