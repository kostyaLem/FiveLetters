using DevExpress.Mvvm;
using FiveLetters.UI.Models;
using Microsoft.Win32;
using System.Windows.Input;

namespace FiveLetters.UI.ViewModels;

/// <summary>
/// Модель для представления SettingsView.xaml
/// </summary>
internal sealed class SettingsViewModel : ViewModelBase
{
    // Выбранный путь до файла со словми
    public string FilePath { get; set; }
    // Кол-во букв в загружаемых словах
    public int WordLength { get; set; }
    // Выбранный режим слов для загрузки
    public LangMode LangMode { get; set; }

    // Флаг нажатия на Ok в диалоговом окне
    public bool IsAccepted { get; set; }

    // Команда нажатия на Ok
    public ICommand AcceptCommand { get; }
    // Команда открытия файла со словами
    public ICommand SelectFileCommand { get; }

    public SettingsViewModel(Settings settings)
    {
        FilePath = settings.FilePath.LocalPath;
        WordLength = settings.LettersCount;
        LangMode = settings.LangMode;

        AcceptCommand = new DelegateCommand(() => IsAccepted = true);
        SelectFileCommand = new DelegateCommand(SelectFile);
    }

    // Обработка команды выбора файла со словами
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

    // Проверить изменились ли настройки после закрытия окна
    public bool IsChanged(Settings settings)
    {
        return (FilePath != settings.FilePath.LocalPath
            || WordLength != settings.LettersCount
            || LangMode != settings.LangMode) 
                && IsAccepted;
    }
}