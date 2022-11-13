using DevExpress.Mvvm;
using FiveLetters.BL.Services;
using FiveLetters.UI.Controls;
using FiveLetters.UI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ISettingsService = FiveLetters.UI.Services.ISettingsService;

namespace FiveLetters.UI.ViewModels;

internal sealed class MainViewModel : BindableBase
{
    private readonly GameProcessor _gameProcessor;
    private readonly ISettingsService _dialogService;
    private RequestedWord _currentAttempt;

    public IReadOnlyList<RequestedWord> Attempts
    {
        get => GetValue<IReadOnlyList<RequestedWord>>(nameof(Attempts));
        set => SetValue(value, nameof(Attempts));
    }

    public bool IsUploading
    {
        get => GetValue<bool>(nameof(IsUploading));
        set => SetValue(value, nameof(IsUploading));
    }

    public Settings Settings
    {
        get => GetValue<Settings>(nameof(Settings));
        set => SetValue(value, nameof(Settings));
    }

    public ICommand LettersClickedCommand { get; }
    public ICommand RemoveClickedCoommand { get; }
    public ICommand EnterClickedCommand { get; }

    public ICommand OpenRatesCommand { get; }
    public ICommand OpenSettingsCommand { get; }
    public ICommand OpenHelpCommand { get; }

    public MainViewModel(
        Settings settings,
        GameProcessor gameProcessor,
        ISettingsService dialogService)
    {
        _gameProcessor = gameProcessor;
        _dialogService = dialogService;

        Settings = settings;

        LettersClickedCommand = new DelegateCommand<LetterRoutedEventArgs>(LettersClicked);
        RemoveClickedCoommand = new DelegateCommand(RemoveClicked);
        EnterClickedCommand = new DelegateCommand(EnterClicked);

        OpenSettingsCommand = new DelegateCommand(OpenSettings);

        RefreshView();
    }

    private void LettersClicked(LetterRoutedEventArgs args)
    {
        _currentAttempt.SetLetter(args.Letter);
    }

    private void RemoveClicked()
    {
        _currentAttempt.RemoveLetter();
    }

    private void EnterClicked()
    {
        var attempt = _gameProcessor.TryGuess(_currentAttempt.Word);
    }

    private void OpenSettings()
    {
        IsUploading = true;

        var result = _dialogService.ShowDialog(Settings);
        if (result is not null)
        {
            Settings = result;
            RefreshView();
        }

        IsUploading = false;
    }

    private void RefreshView()
    {
        var words = Enumerable.Range(0, 6).Select(x => new RequestedWord(Settings.LettersCount));
        Attempts = new List<RequestedWord>(words);
        _currentAttempt = Attempts[0];
    }
}
