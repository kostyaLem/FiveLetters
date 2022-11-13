using DevExpress.Mvvm;
using FiveLetters.UI.Models;

namespace FiveLetters.UI.Services;

internal interface ISettingsService
{
    public Settings ShowDialog(Settings settings);
}
