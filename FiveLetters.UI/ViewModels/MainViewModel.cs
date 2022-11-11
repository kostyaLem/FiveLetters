using DevExpress.Mvvm;
using FiveLetters.BL.Services;
using FiveLetters.UI.Controls;
using FiveLetters.UI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using IDialogService = FiveLetters.UI.Services.IDialogService;

namespace FiveLetters.UI.ViewModels;

internal class MainViewModel : BindableBase
{
    private readonly SettingsViewModel _settingsViewModel;
    private readonly GameProcessor _gameProcessor;
    private readonly IDialogService _dialogService;

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

    public int LettersCount
    {
        get => GetValue<int>(nameof(LettersCount));
        set => SetValue(value, nameof(LettersCount));
    }

    public LangMode LangMode
    {
        get => GetValue<LangMode>(nameof(LangMode));
        set => SetValue(value, nameof(LangMode));
    }

    public ICommand LettersClickedCommand { get; }

    public ICommand OpenRatesCommand { get; }
    public ICommand OpenSettingsCommand { get; }
    public ICommand OpenHelpCommand { get; }

    public MainViewModel(
        SettingsViewModel settingsViewModel,
        GameProcessor gameProcessor,
        IDialogService dialogService)
    {
        _settingsViewModel = settingsViewModel;
        _gameProcessor = gameProcessor;
        _dialogService = dialogService;

        LettersCount = _settingsViewModel.WordLength;
        LangMode = _settingsViewModel.LangMode;

        LettersClickedCommand = new DelegateCommand<LetterRoutedEventArgs>(LettersClicked);
        OpenSettingsCommand = new DelegateCommand(OpenSettings);

        Attempts = new List<RequestedWord>(Enumerable.Range(0, 6).Select(x => new RequestedWord(LettersCount)));
    }

    private void LettersClicked(LetterRoutedEventArgs args)
    {
        Debug.WriteLine(args.Letter);
    }

    private void OpenSettings()
    {
        IsUploading = true;

        _dialogService.ShowDialog<SettingsViewModel>();

        if (_settingsViewModel.IsAccepted)
        {
            LettersCount = _settingsViewModel.WordLength;
            LangMode = _settingsViewModel.LangMode;
            Attempts = new List<RequestedWord>(
                Enumerable.Range(0, 6).Select(x => new RequestedWord(LettersCount)));
        }

        IsUploading = false;
    }
}
