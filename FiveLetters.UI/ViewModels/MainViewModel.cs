using DevExpress.Mvvm;
using FiveLetters.UI.Controls;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services.Interfaces;
using System;
using System.CodeDom.Compiler;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FiveLetters.UI.ViewModels;

internal sealed class MainViewModel : BindableBase
{
    private readonly ISettingsService _dialogService;
    private readonly IGameProcessor _gameProcessor;

    public IGameState GameState => _gameProcessor;

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

    public ICommand ResetCommand { get; }
    public ICommand OpenSettingsCommand { get; }
    public ICommand OpenHelpCommand { get; }

    public MainViewModel(
        Settings settings,
        IGameProcessor gameProcessor,
        ISettingsService dialogService)
    {
        _gameProcessor = gameProcessor;
        _dialogService = dialogService;

        Settings = settings;

        LoadViewDataCommand = new AsyncCommand(RefreshView);
        LettersClickedCommand = new DelegateCommand<LetterRoutedEventArgs>(LettersClicked);
        RemoveClickedCoommand = new DelegateCommand(RemoveClicked);
        EnterClickedCommand = new DelegateCommand(EnterClicked);

        ResetCommand = new AsyncCommand(RefreshView);
        OpenSettingsCommand = new AsyncCommand(OpenSettings);
    }

    private void LettersClicked(LetterRoutedEventArgs args)
    {
        _gameProcessor.AddLetter(args.Letter);
    }

    private void RemoveClicked()
    {
        _gameProcessor.RemoveLetter();
    }

    private void EnterClicked()
    {
        var attemptStatus = _gameProcessor.CheckWord();

        if (attemptStatus != AttemptStatus.CanRepeat)
        {

        }

        if (attemptStatus == AttemptStatus.Win)
        {
            // show message box if guessed and begin new game then
        }
        else
        {

        }
    }

    private async Task OpenSettings()
    {
        await Execute(async () =>
        {
            var result = _dialogService.ShowDialog(Settings);
            if (result is not null)
            {
                Settings = result;

                await RefreshView();
            }
        });
    }

    private async Task RefreshView()
    {
        await Execute(() => _gameProcessor.ReloadWords(Settings));
    }

    private async Task Execute(Func<Task> task)
    {
        IsUploading = true;

        await task();

        IsUploading = false;
    }
}
