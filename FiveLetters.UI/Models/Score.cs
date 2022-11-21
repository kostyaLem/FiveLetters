using DevExpress.Mvvm;

namespace FiveLetters.UI.Models;

internal sealed class Score : BindableBase
{
    public int Win
    {
        get => GetValue<int>(nameof(Win));
        set => SetValue(value, nameof(Win));
    }

    public int Lose
    {
        get => GetValue<int>(nameof(Lose));
        set => SetValue(value, nameof(Lose));
    }

    public int Total
    {
        get => GetValue<int>(nameof(Total));
        set => SetValue(value, nameof(Total));
    }

    public void Reset(int total)
    {
        Win = Lose = 0;
        Total = total;
    }
}