using System.Collections.ObjectModel;
using System.Linq;

namespace FiveLetters.UI.Models;

internal class RequestedWord
{
    public ObservableCollection<RequestedLetter> Letters { get; }

    public bool IsFinished => Letters.All(x => x.SelectedLetter is not null);

    public string Word => new string(Letters.Select(x => x.SelectedLetter ?? '\0').ToArray()); // TODO: fix

    // For debug
    public RequestedWord()
    {
        var letters = Enumerable.Range(0, 7).Select(x => new RequestedLetter());
        Letters = new ObservableCollection<RequestedLetter>(letters);
    }

    public RequestedWord(int wordLength)
    {
        var letters = Enumerable.Range(0, wordLength).Select(x => new RequestedLetter());
        Letters = new ObservableCollection<RequestedLetter>(letters);
    }

    public bool SetLetter(char letter)
    {
        var firstLetter = Letters.FirstOrDefault(x => x.SelectedLetter is null);

        if (firstLetter != null)
        {
            firstLetter.SelectedLetter = letter;
        }

        return IsFull();
    }

    public bool RemoveLetter()
    {
        var firstLetter = Letters.LastOrDefault(x => x.SelectedLetter is not null);

        if (firstLetter != null)
        {
            firstLetter.SelectedLetter = default;
        }

        return !HasEmpty();
    }

    private bool IsFull()
    {
        return Letters.All(x => x.SelectedLetter is not null);
    }

    private bool HasEmpty()
    {
        return Letters.Any(x => x.SelectedLetter is null);
    }
}
