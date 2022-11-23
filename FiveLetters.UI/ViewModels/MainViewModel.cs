using DevExpress.Mvvm;
using FiveLetters.UI.Controls;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services;
using FiveLetters.UI.Services.Interfaces;
using FiveLetters.UI.Views;
using System;
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
        OpenHelpCommand = new DelegateCommand(OpenHelp);
    }

    private void LettersClicked(LetterRoutedEventArgs args)
    {
        _gameProcessor.AddLetter(args.Letter);
    }

    private void RemoveClicked()
    {
        _gameProcessor.RemoveLetter();
    }

    private async void EnterClicked()
    {
        Execute(() =>
        {
            var attemptStatus = _gameProcessor.CheckWord();

            if (attemptStatus == AttemptStatus.Win)
            {
                InfoBox.ShowWin();
            }
            else if (attemptStatus == AttemptStatus.Lose)
            {
                InfoBox.ShowLose();
            }

            if (attemptStatus != AttemptStatus.CanRepeat)
            {
                if (!_gameProcessor.NextWord())
                {
                    InfoBox.ShowEnd();
                }
            }
        });
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

    private void OpenHelp()
    {
        Execute(() =>
        {
            new AboutView().ShowDialog();
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

    private async void Execute(Action task)
    {
        IsUploading = true;

        task();

        IsUploading = false;
    }
}
