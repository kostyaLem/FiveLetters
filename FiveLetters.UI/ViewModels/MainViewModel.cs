using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using FiveLetters.BL.Services;
using FiveLetters.UI.Controls;
using FiveLetters.UI.Mappers;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

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

    public ICommand LoadViewDataCommand { get; }

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

        LoadViewDataCommand = new AsyncCommand(RefreshView);
        LettersClickedCommand = new DelegateCommand<LetterRoutedEventArgs>(LettersClicked);
        RemoveClickedCoommand = new DelegateCommand(RemoveClicked);
        EnterClickedCommand = new DelegateCommand(EnterClicked);

        OpenSettingsCommand = new AsyncCommand(OpenSettings);
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

        foreach (var (current, updated) in _currentAttempt.Letters.Zip(attempt.Letters))
        {
            current.CellStyle = CellStyleMapper.Map(updated.Status);
        }

        // show message box if guessed and begin new game then
    }

    private async Task OpenSettings()
    {
        IsUploading = true;

        var result = _dialogService.ShowDialog(Settings);
        if (result is not null)
        {
            Settings = result;

            await RefreshView();
        }

        IsUploading = false;
    }

    private async Task RefreshView()
    {
        await _gameProcessor.ResetSettings(SettingsMapper.Map(Settings));

        var words = Enumerable.Range(0, 6).Select(x => new RequestedWord(Settings.LettersCount));
        Attempts = new List<RequestedWord>(words);
        _currentAttempt = Attempts[0];
    }
}
