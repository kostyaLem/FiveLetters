using DevExpress.Mvvm;

namespace FiveLetters.UI.Models;

internal class RequestedLetter : BindableBase
{
    private readonly string _word;
    private readonly char _letter;

    public CellStyle CellStyle
    {
        get => GetValue<CellStyle>(nameof(CellStyle));
        set => SetValue(value, nameof(CellStyle));
    }
    public char SelectedLetter
    {
        get => GetValue<char>(nameof(SelectedLetter));
        set => SetValue(value, ch => SetStyle(ch), nameof(SelectedLetter));
    }

    public RequestedLetter(string word, char letter)
    {
        _word = word;
        _letter = letter;
    }

    private void SetStyle(char ch)
    {
        CellStyle = ch switch
        {
            _ when _letter == ch => CellStyle.Guessed,
            _ when _word.Contains(ch) => CellStyle.Nearly,
            _ => CellStyle.Wrong
        };
    }
}
