using DevExpress.Mvvm;
using FiveLetters.UI.Controls;
using System.Diagnostics;
using System.Windows.Input;

namespace FiveLetters.UI.ViewModels;

internal class MainViewModel : BindableBase
{
    public int LettersCount
    {
        get => GetValue<int>(nameof(LettersCount));
        set => SetValue(value, nameof(LettersCount));
    }

    public ICommand LettersClickedCommand { get; }

    public ICommand OpenRates { get; }
    public ICommand OpenSettings { get; }
    public ICommand OpenHelp { get; }

    public MainViewModel()
    {
        LettersClickedCommand = new DelegateCommand<LetterRoutedEventArgs>(LettersClicked);
    }

    private void LettersClicked(LetterRoutedEventArgs args)
    {
        Debug.WriteLine(args.Letter);
    }
}

internal sealed class SettingsViewModel : BindableBase
{
    public string FilePath { get; set; }
    public int WordLength { get; set; }

    public SettingsViewModel()
    {

    }
}