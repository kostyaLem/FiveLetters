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

/// <summary>
/// Модель для представления MainView.xaml
/// </summary>
internal sealed class MainViewModel : BindableBase
{
    // Сервис для работы с настройками
    private readonly ISettingsService _dialogService;
    // Сервис управления ходом игры
    private readonly IGameProcessor _gameProcessor;

    // Попытки пользователя
    public IGameState GameState => _gameProcessor;

    // Индикатор отображения лоадера
    public bool IsUploading
    {
        get => GetValue<bool>(nameof(IsUploading));
        set => SetValue(value, nameof(IsUploading));
    }

    // Настройки игры
    public Settings Settings
    {
        get => GetValue<Settings>(nameof(Settings));
        set => SetValue(value, nameof(Settings));
    }

    // Команда, вызывающаяся при открытии окна игры
    public ICommand LoadViewDataCommand { get; }

    // Команда нажатия на кнопку
    public ICommand LettersClickedCommand { get; }
    // Команда нажатия на кнопку удаления буквы
    public ICommand RemoveClickedCoommand { get; }
    // Команда нажатия на кнопку ввода слова
    public ICommand EnterClickedCommand { get; }

    // Команда сброса игры в начальное состояние
    public ICommand ResetCommand { get; }
    // Команда открытия настроек игры
    public ICommand OpenSettingsCommand { get; }
    // Команда открытия справки об игре
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

    // Обработка команды нажатия на кнопку
    private void LettersClicked(LetterRoutedEventArgs args)
    {
        _gameProcessor.AddLetter(args.Letter);
    }

    // Обработка команды нажатия на кнопку удаления буквы
    private void RemoveClicked()
    {
        _gameProcessor.RemoveLetter();
    }

    // Обработка команды нажатия на кнопку ввода слова
    private async void EnterClicked()
    {
        Execute(() =>
        {
            // Получить результат проверки введённых букв
            var attemptStatus = _gameProcessor.CheckWord();

            // Отобразить результат обраотки
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

    // Обработка команды открытия настроек игры
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

    // Обработка команды открытия окна со справкой
    private void OpenHelp()
    {
        Execute(() =>
        {
            new AboutView().ShowDialog();
        });
    }

    // Обработка команды сброса игры в начальное состояние
    private async Task RefreshView()
    {
        await Execute(() => _gameProcessor.ReloadWords(Settings));
    }

    // Вспомогательный метод для выполнения асинхронных методов
    private async Task Execute(Func<Task> task)
    {
        IsUploading = true;

        await task();

        IsUploading = false;
    }

    // Вспомогательный метод для выполнения синхронных методов
    private async void Execute(Action task)
    {
        IsUploading = true;

        task();

        IsUploading = false;
    }
}
