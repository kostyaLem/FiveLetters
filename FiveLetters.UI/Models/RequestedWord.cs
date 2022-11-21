using System.Collections.ObjectModel;
using System.Linq;

namespace FiveLetters.UI.Models;

internal class RequestedWord
{
    public ObservableCollection<RequestedLetter> Letters { get; }

    public bool IsFinished => Letters.All(x => x.SelectedLetter is not null);

    public string Word => new string(Letters.Select(x => x.SelectedLetter ?? '\0').ToArray()); // TODO: fix

    public RequestedWord(int wordLength)
    {
        var letters = Enumerable.Range(0, wordLength).Select(x => new RequestedLetter());
        Letters = new ObservableCollection<RequestedLetter>(letters);
    }

    public void SetLetter(char letter)
    {
        var firstLetter = Letters.FirstOrDefault(x => x.SelectedLetter is null);

        if (firstLetter != null)
        {
            firstLetter.SelectedLetter = letter;
        }
    }

    public void RemoveLetter()
    {
        var firstLetter = Letters.LastOrDefault(x => x.SelectedLetter is not null);

        if (firstLetter != null)
        {
            firstLetter.SelectedLetter = default;
        }
    }
}
