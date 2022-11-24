using DevExpress.Mvvm;

namespace FiveLetters.UI.Models;

/// <summary>
/// Общий счёт игры
/// </summary>
internal sealed class Score : BindableBase
{
    // Кол-во угаданных слов
    public int Win
    {
        get => GetValue<int>(nameof(Win));
        set => SetValue(value, nameof(Win));
    }

    // Кол-во проигрышей
    public int Lose
    {
        get => GetValue<int>(nameof(Lose));
        set => SetValue(value, nameof(Lose));
    }

    // Кол-во оставшихся слов
    public int Total
    {
        get => GetValue<int>(nameof(Total));
        set => SetValue(value, nameof(Total));
    }

    // Сбросить счётчик
    public void Reset(int total)
    {
        Win = Lose = 0;
        Total = total;
    }
}