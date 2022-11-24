using DevExpress.Mvvm;
using FiveLetters.BL.Models;
using FiveLetters.BL.Services;
using FiveLetters.UI.Mappers;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiveLetters.UI.Services;

/// <summary>
/// Класс для управления ходом игры
/// </summary>
internal sealed class GameProcessor : BindableBase, IGameProcessor
{
    // Кол-во попыток на отгадывание
    private const int _totalAttempts = 6;
    // Текущее кол-во попыток
    private int _countOfAttempts = 0;

    // Сервис для управления словами
    private readonly WordsManager _wordsManager;
    // Текущее слово для заполнения на интерфейсе
    private RequestedWord _currentAttempt;
    // Последние настройки игры
    private Settings _settings;

    // Очки игры
    public Score Score { get; }

    // Флаг доступа нажатия клавиатуру
    public bool CanInput
    {
        get => GetValue<bool>(nameof(CanInput));
        set => SetValue(value, nameof(CanInput));
    }

    // Флаг доступа нажатия на кнопку Enter
    public bool CanEnter
    {
        get => GetValue<bool>(nameof(CanEnter));
        set => SetValue(value, nameof(CanEnter));
    }

    // Флаг доступа нажатия на кнопку удаления буквы
    public bool CanRemove
    {
        get => GetValue<bool>(nameof(CanRemove));
        set => SetValue(value, nameof(CanRemove));
    }

    // Список строк-слов на интерфейсе
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

    // Добавить символ в слово
    public void AddLetter(char letter)
    {
        _currentAttempt.SetLetter(letter);

        CanEnter = _currentAttempt.IsFinished;
    }

    // Удалить символ из слова
    public void RemoveLetter()
    {
        _currentAttempt.RemoveLetter();

        CanEnter = _currentAttempt.IsFinished;
    }

    // Сравнить введённое слово
    public AttemptStatus CheckWord()
    {
        // Увеличить кол-во попыток
        _countOfAttempts++;

        // Получить состояние выбранных букв
        var states = UpdateCells();

        // Вывести результат попытки
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

        // Обновить статистику и доступность кнопок
        Score.Lose++;
        CanEnter = CanRemove = false;

        return AttemptStatus.Lose;
    }

    // Получить результат проверки букв и обновить статусы ячеек с буквами на UI
    private IReadOnlyList<LetterState> UpdateCells()
    {
        var states = _wordsManager.TryGuess(_currentAttempt.Word);

        foreach (var (current, updated) in _currentAttempt.Letters.Zip(states))
        {
            current.CellStyle = CellStyleMapper.Map(updated.Status);
        }

        return states;
    }

    // Перейти к следующему слову
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

    // Загрузить слова в соответствии с настройками
    public async Task ReloadWords(Settings settings)
    {
        _settings = settings;
        await _wordsManager.ResetSettings(SettingsMapper.Map(settings));

        Score.Reset(_wordsManager.WordsCount);
        Reset();
    }

    // Сбросить игру и начать заново
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
