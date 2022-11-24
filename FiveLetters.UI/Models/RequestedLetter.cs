using DevExpress.Mvvm;

namespace FiveLetters.UI.Models;

/// <summary>
/// Модель буквы на интерфейсе
/// </summary>
internal sealed class RequestedLetter : BindableBase
{
    // Статус ячейки с буквой
    public CellStyle CellStyle
    {
        get => GetValue<CellStyle>(nameof(CellStyle));
        set => SetValue(value, nameof(CellStyle));
    }

    // Введённая буква
    public char? SelectedLetter
    {
        get => GetValue<char?>(nameof(SelectedLetter));
        set => SetValue(value, nameof(SelectedLetter));
    }
}
