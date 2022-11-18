using DevExpress.Mvvm;
using FiveLetters.BL.Services;
using FiveLetters.UI.Mappers;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveLetters.UI.Services;

internal sealed class GameProcessor : BindableBase, IGameState, IGameProcessor
{
    private readonly WordsManager _wordsManager;
    private RequestedWord _currentAttempt;
    private Settings _settings;

    public IReadOnlyList<RequestedWord> Attempts
    {
        get => GetValue<IReadOnlyList<RequestedWord>>(nameof(Attempts));
        set => SetValue(value, nameof(Attempts));
    }

    public GameProcessor(WordsManager wordsManager)
    {
        _wordsManager = wordsManager;
    }

    public void AddLetter(char letter)
    {
        _currentAttempt.SetLetter(letter);
    }

    public void RemoveLetter()
    {
        _currentAttempt.RemoveLetter();
    }

    public bool CheckWord()
    {
        var states = _wordsManager.TryGuess(_currentAttempt.Word);
        var isGuessed = states.All(x => x.Status == BL.Models.LetterStatus.Guessed);

        foreach (var (current, updated) in _currentAttempt.Letters.Zip(states))
        {
            current.CellStyle = CellStyleMapper.Map(updated.Status);
        }

        return isGuessed;
    }

    public bool NextWord()
    {
        if (_wordsManager.MoveNext())
        {
            Reset();
            return true;
        }

        return false;
    }

    public async Task ReloadWords(Settings settings)
    {
        _settings = settings;
        await _wordsManager.ResetSettings(SettingsMapper.Map(settings));

        Reset();
    }

    private void Reset()
    {
        Attempts = Enumerable.Range(0, 6)
            .Select(x => new RequestedWord(_settings.LettersCount))
            .ToList();

        _currentAttempt = Attempts[0];
    }
}
