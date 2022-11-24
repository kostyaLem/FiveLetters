using System.Collections.ObjectModel;
using System.Linq;

namespace FiveLetters.UI.Models;

/// <summary>
/// Модель слова с введёнными буквами
/// </summary>
internal class RequestedWord
{
    // Коллекция букв в слове
    public ObservableCollection<RequestedLetter> Letters { get; }

    // Флаг заполненности букв в слове
    public bool IsFinished => Letters.All(x => x.SelectedLetter is not null);

    // Введённые буквы в виде строки
    public string Word => new string(Letters.Select(x => x.SelectedLetter ?? '\0').ToArray()); // TODO: fix

    public RequestedWord(int wordLength)
    {
        var letters = Enumerable.Range(0, wordLength).Select(x => new RequestedLetter());
        Letters = new ObservableCollection<RequestedLetter>(letters);
    }

    // Добавить символ в конец слова
    public void SetLetter(char letter)
    {
        var firstLetter = Letters.FirstOrDefault(x => x.SelectedLetter is null);

        if (firstLetter != null)
        {
            firstLetter.SelectedLetter = letter;
        }
    }

    // Удалить символ с конца слова
    public void RemoveLetter()
    {
        var firstLetter = Letters.LastOrDefault(x => x.SelectedLetter is not null);

        if (firstLetter != null)
        {
            firstLetter.SelectedLetter = default;
        }
    }
}
