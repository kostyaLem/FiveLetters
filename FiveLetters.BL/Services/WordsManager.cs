using FiveLetters.BL.Models;
using FiveLetters.BL.Models.Settings;
using FiveLetters.BL.Services.Readers;

namespace FiveLetters.BL.Services;

/// <summary>
/// Класс для управления состоянием игры
/// </summary>
public sealed class WordsManager
{
    // Генератор псевдослучайных чисел для перемешивания слов в списке
    private static readonly Random _rnd = new Random();

    // Сервис чтения слов из файла
    private readonly WordReader _wordReader;

    // Очередь новых слов
    private Queue<string> _wordsQueue;
    // Текущее слово для угадывания
    private string _currentWord;

    // Количество слов в очереди
    public int WordsCount => _wordsQueue.Count;

    public WordsManager(WordReader wordReader)
    {
        _wordReader = wordReader;
    }

    // Загрузить слова в соответствии с настройками
    public async Task ResetSettings(WordReaderSettings settings)
    {
        // Загрузить слова
        var words = await _wordReader.ReadWords(settings);
        // Перемешать список
        var shakedWords = words.OrderBy(x => _rnd.Next());

        // Сбросить состояние игры до начального
        _currentWord = default;
        _wordsQueue = new Queue<string>(shakedWords);

        // Поместить первое слово на отгадывание
        if (_wordsQueue.TryDequeue(out var word))
        {
            _currentWord = word;
        }
    }

    public bool MoveNext()
    {
        // Перейти к следующему слову из очереди
        if (_wordsQueue.TryDequeue(out var nextWord))
        {
            _currentWord = nextWord;
            return true;
        }

        // Вернуть false, если все слова кончились
        return false;
    }

    // Проверка введённого слова
    public IReadOnlyList<LetterState> TryGuess(string word)
    {
        // Проверка соответствия длин введённого и загаданного слова
        Validate(word);

        // Проверить попытку
        return CheckGuess(word);
    }

    private void Validate(string word)
    {
        if (word.Length != _currentWord.Length)
        {
            throw new Exception();
        }
    }

    private IReadOnlyList<LetterState> CheckGuess(string word)
    {
        var states = new List<LetterState>();

        // Проходимся по буквам введённого слова и угадываемого
        foreach (var (attemptCh, currentCh) in word.Zip(_currentWord))
        {
            // Если символ и позиция угаданы
            if (attemptCh == currentCh)
            {
                // Пометить как угаданный
                states.Add(new(attemptCh, LetterStatus.Guessed));
            }
            // Если символа вообще нет в слове
            else if (!_currentWord.Contains(attemptCh))
            {
                // Пометить как ошибочный
                states.Add(new(attemptCh, LetterStatus.Wrong));
            }
            // Если символ есть, но не на своей позиции
            else
            {
                // Помечаем как неугаданные для последующей обработки
                states.Add(new(attemptCh, LetterStatus.NotGuessed));
            }
        }

        // Проходимся по неугаданным символам.
        // Здесь будут только символы, которые стоят не на своих местах
        foreach (var state in states.Where(x => x.Status == LetterStatus.NotGuessed))
        {
            // Считаем количество символов, которые угаданы или есть в слове, но не на своей позиции
            var existingCount = states.Count(x => x.Ch == state.Ch 
                && (x.Status == LetterStatus.Guessed || x.Status == LetterStatus.Nearly));
            // Считаем общее количество проверяемой буквы в слове
            var totalCount = _currentWord.Count(x => x == state.Ch);

            // Пометить как "Рядом", если проверяемой буквы меньше чем её общее количество в слове
            state.Status = existingCount < totalCount ? LetterStatus.Nearly : LetterStatus.Wrong;
        }
        
        // Вернуть состояние каждой буквыы
        return states;

        // Пример
        // Слово:             АКЦИЯ
        // Пользователь ввёл: АКАЦИ
        // 0 - Правильно
        // 1 - Правильно
        // 2 - Неправильно, так как больше букв А в слове нет
        // 3 - Правильно, но на другой позции
        // 4 - Правильно, но на другой позиции
    }
}