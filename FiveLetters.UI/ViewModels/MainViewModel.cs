using DevExpress.Mvvm;
using FiveLetters.UI.Controls;
using FiveLetters.UI.Models;
using System.Diagnostics;
using System.Windows.Input;
using IDialogService = FiveLetters.UI.Services.IDialogService;

namespace FiveLetters.UI.ViewModels;

internal class MainViewModel : BindableBase
{
    private readonly SettingsViewModel _settingsViewModel;
    private readonly IDialogService _dialogService;

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

    public MainViewModel(SettingsViewModel settingsViewModel, IDialogService dialogService)
    {
        _settingsViewModel = settingsViewModel;
        _dialogService = dialogService;

        LettersCount = _settingsViewModel.WordLength;
        LangMode = _settingsViewModel.LangMode;

        LettersClickedCommand = new DelegateCommand<LetterRoutedEventArgs>(LettersClicked);
        OpenSettingsCommand = new DelegateCommand(OpenSettings);
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

            // Load words and refresh UI
        }

        IsUploading = false;
    }
}
