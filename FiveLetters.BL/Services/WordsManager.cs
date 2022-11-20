using FiveLetters.BL.Models;
using FiveLetters.BL.Models.Settings;
using FiveLetters.BL.Services.Readers;

namespace FiveLetters.BL.Services;

public sealed class WordsManager
{
    private readonly WordReader _wordReader;

    private Queue<string> _wordsQueue;
    private string _currentWord;

    public WordsManager(WordReader wordReader)
    {
        _wordReader = wordReader;
    }

    public async Task ResetSettings(WordReaderSettings settings)
    {
        var words = await _wordReader.ReadWords(settings);

        _wordsQueue = new Queue<string>(words);
        _currentWord = _wordsQueue.Dequeue();
    }

    public bool MoveNext()
    {
        if (_wordsQueue.TryDequeue(out var nextWord))
        {
            _currentWord = nextWord;
            return true;
        }

        return false;
    }

    public IReadOnlyList<LetterState> TryGuess(string word)
    {
        Validate(word);

        return CheckGuess(word);
    }

    private void Validate(string word)
    {
        if (word.Length != _currentWord.Length)
        {
            throw new Exception();
        }
    }

    private IReadOnlyList<LetterState> CheckGuess(string word)
    {
        var states = new List<LetterState>();

        foreach (var (attemptCh, currentCh) in word.Zip(_currentWord))
        {
            var letterStatus = LetterStatus.Wrong;

            if (attemptCh == currentCh)
            {
                letterStatus = LetterStatus.Guessed;
            }
            else if (_currentWord.Contains(attemptCh))
            {
                var existingCount = states.Count(x => x.Ch == currentCh);
                var totalCount = _currentWord.Count(x => x == currentCh);

                if (existingCount < totalCount)
                {
                    letterStatus = LetterStatus.Nearly;
                }
            }

            states.Add(new(currentCh, letterStatus));
        }

        return states;
    }
}