using HandyControl.Tools.Extension;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiveLetters.UI.Models;

internal class RequestedWord
{
    public ObservableCollection<RequestedLetter> Letters { get; }

    public bool IsFinished => Letters.All(x => x.SelectedLetter is not null);

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
}
