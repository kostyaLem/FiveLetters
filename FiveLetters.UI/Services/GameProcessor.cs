﻿using DevExpress.Mvvm;
using FiveLetters.BL.Models;
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
    private const int _totalAttempts = 6;
    private int _countOfAttempts = 0;

    private readonly WordsManager _wordsManager;
    private RequestedWord _currentAttempt;
    private Settings _settings;

    public Score Score { get; }

    public bool CanInput
    {
        get => GetValue<bool>(nameof(CanInput));
        set => SetValue(value, nameof(CanInput));
    }

    public bool CanEnter
    {
        get => GetValue<bool>(nameof(CanEnter));
        set => SetValue(value, nameof(CanEnter));
    }

    public bool CanRemove
    {
        get => GetValue<bool>(nameof(CanRemove));
        set => SetValue(value, nameof(CanRemove));
    }

    public IReadOnlyList<RequestedWord> Attempts
    {
        get => GetValue<IReadOnlyList<RequestedWord>>(nameof(Attempts));
        set => SetValue(value, nameof(Attempts));
    }

    public GameProcessor(WordsManager wordsManager)
    {
        _wordsManager = wordsManager;

        Score = new Score();
    }

    public void AddLetter(char letter)
    {
        _currentAttempt.SetLetter(letter);

        CanEnter = _currentAttempt.IsFinished;
    }

    public void RemoveLetter()
    {
        _currentAttempt.RemoveLetter();

        CanEnter = _currentAttempt.IsFinished;
    }

    public AttemptStatus CheckWord()
    {
        _countOfAttempts++;

        var states = UpdateCells();

        if (states.All(x=>x.Status == LetterStatus.Guessed))
        {
            CanEnter = CanRemove = false;
            Score.Win++;
            return AttemptStatus.Win;
        }

        if (_countOfAttempts < Attempts.Count)
        {
            CanEnter = false;
            _currentAttempt = Attempts[_countOfAttempts];
            return AttemptStatus.CanRepeat;
        }

        Score.Lose++;
        CanEnter = CanRemove = false;

        return AttemptStatus.Lose;
    }

    private IReadOnlyList<LetterState> UpdateCells()
    {
        var states = _wordsManager.TryGuess(_currentAttempt.Word);

        foreach (var (current, updated) in _currentAttempt.Letters.Zip(states))
        {
            current.CellStyle = CellStyleMapper.Map(updated.Status);
        }

        return states;
    }

    public bool NextWord()
    {
        if (_wordsManager.MoveNext())
        {
            Reset();
            Score.Total--;
            return true;
        }

        return false;
    }

    public async Task ReloadWords(Settings settings)
    {
        _settings = settings;
        await _wordsManager.ResetSettings(SettingsMapper.Map(settings));

        Score.Reset(_wordsManager.WordsCount);
        Reset();
    }

    private void Reset()
    {
        Attempts = Enumerable.Range(0, _totalAttempts)
            .Select(x => new RequestedWord(_settings.LettersCount))
            .ToList();

        _countOfAttempts = 0;
        _currentAttempt = Attempts[_countOfAttempts];

        CanRemove = true;
        CanEnter = false;
        CanInput = _wordsManager.WordsCount > 0;
    }
}
