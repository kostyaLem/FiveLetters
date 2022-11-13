using DevExpress.Mvvm;
using FiveLetters.UI.Models;
using Microsoft.Win32;
using System.Windows.Input;

namespace FiveLetters.UI.ViewModels;

internal sealed class SettingsViewModel : ViewModelBase
{
    public string FilePath { get; set; }
    public int WordLength { get; set; }
    public LangMode LangMode { get; set; }

    public bool IsAccepted { get; set; }

    public ICommand AcceptCommand { get; }
    public ICommand SelectFileCommand { get; }

    public SettingsViewModel(Settings settings)
    {
        FilePath = settings.FilePath.LocalPath;
        WordLength = settings.LettersCount;
        LangMode = settings.LangMode;

        AcceptCommand = new DelegateCommand(() => IsAccepted = true);
        SelectFileCommand = new DelegateCommand(SelectFile);
    }

    private void SelectFile()
    {
        var dialog = new OpenFileDialog();
        dialog.Filter = "TXT (*.txt)|*.txt";
        // Открыть диалоговое окно выбора файла
        var result = dialog.ShowDialog();

        if (result.HasValue && result.Value)
        {
            FilePath = dialog.FileName;
            RaisePropertiesChanged(nameof(FilePath));
        }
    }

    public bool IsChanged(Settings settings)
    {
        return (FilePath != settings.FilePath.LocalPath
            || WordLength != settings.LettersCount
            || LangMode != settings.LangMode) 
                && IsAccepted;
    }
}