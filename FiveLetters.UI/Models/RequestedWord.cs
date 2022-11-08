using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace FiveLetters.UI.Models;

internal class RequestedWord
{
    public ObservableCollection<RequestedLetter> Characters { get; }

    public RequestedWord(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
        {
            throw new ArgumentNullException(nameof(word));
        }

        Characters = new(word.Select(ch => new RequestedLetter(word, ch)));
    }
}
