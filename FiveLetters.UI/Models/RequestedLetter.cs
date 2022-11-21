﻿using DevExpress.Mvvm;

namespace FiveLetters.UI.Models;

internal sealed class RequestedLetter : BindableBase
{
    public CellStyle CellStyle
    {
        get => GetValue<CellStyle>(nameof(CellStyle));
        set => SetValue(value, nameof(CellStyle));
    }

    public char? SelectedLetter
    {
        get => GetValue<char?>(nameof(SelectedLetter));
        set => SetValue(value, nameof(SelectedLetter));
    }
}
