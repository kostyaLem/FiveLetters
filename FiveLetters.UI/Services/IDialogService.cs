using DevExpress.Mvvm;

namespace FiveLetters.UI.Services;

internal interface IDialogService
{
    public void ShowDialog<T>() where T : ViewModelBase;
}
