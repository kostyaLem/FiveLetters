using DevExpress.Mvvm;
using System.Windows.Input;

namespace FiveLetters.UI.ViewModels;

internal class MainViewModel : BindableBase
{
    public int LettersCount
    {
        get => GetValue<int>(nameof(LettersCount));
        set => SetValue(value, nameof(LettersCount));
    }

    public ICommand OpenRates { get; }
    public ICommand OpenSettings { get; }
    public ICommand OpenHelp { get; }

    public MainViewModel()
    {

    }
}
