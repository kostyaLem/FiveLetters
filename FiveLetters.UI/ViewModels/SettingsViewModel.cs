using DevExpress.Mvvm;
using FiveLetters.UI.Models;
using System.Windows.Input;

namespace FiveLetters.UI.ViewModels;

internal sealed class SettingsViewModel : ViewModelBase
{
    public string FilePath { get; set; }
    public int WordLength { get; set; }
    public LangMode Language { get; set; }

    public bool IsAccepted { get; private set; } 

    public ICommand AcceptCommand { get; }

    public SettingsViewModel(Settings settings)
    {
        FilePath = settings.FilePath.LocalPath;
        WordLength = settings.WordLength;
        Language = settings.LangMode;

        AcceptCommand = new DelegateCommand(() => IsAccepted = true);
    }
}