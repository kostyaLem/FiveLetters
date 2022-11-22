using FiveLetters.BL.Models;
using FiveLetters.BL.Models.Settings;
using FiveLetters.BL.Services.Readers;

namespace FiveLetters.BL.Services;

public sealed class WordsManager
{
    private static readonly Random _rnd = new Random();

    private readonly WordReader _wordReader;

    private Queue<string> _wordsQueue;
    private string _currentWord;

    public int WordsCount => _wordsQueue.Count;

    public WordsManager(WordReader wordReader)
    {
        _wordReader = wordReader;
    }

    public async Task ResetSettings(WordReaderSettings settings)
    {
        var words = await _wordReader.ReadWords(settings);
        var shakedWords = words.OrderBy(x => _rnd.Next());

        _currentWord = default;
        _wordsQueue = new Queue<string>(shakedWords);

        if (_wordsQueue.TryDequeue(out var word))
        {
            _currentWord = word;
        }
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
            if (attemptCh == currentCh)
            {
                states.Add(new(attemptCh, LetterStatus.Guessed));
            }
            else if (!_currentWord.Contains(attemptCh))
            {
                states.Add(new(attemptCh, LetterStatus.Wrong));
            }
            else
            {
                states.Add(new(attemptCh, LetterStatus.NotGuessed));
            }
        }

        foreach (var state in states.Where(x => x.Status == LetterStatus.NotGuessed))
        {
            var existingCount = states.Count(x => x.Ch == state.Ch 
                && (x.Status == LetterStatus.Guessed || x.Status == LetterStatus.Nearly));
            var totalCount = _currentWord.Count(x => x == state.Ch);

            state.Status = existingCount < totalCount ? LetterStatus.Nearly : LetterStatus.Wrong;
        }

        return states;
    }
}