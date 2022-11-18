using DevExpress.Mvvm;
using FiveLetters.UI.Models;

namespace FiveLetters.UI.Services.Interfaces;

internal interface ISettingsService
{
    public Settings ShowDialog(Settings settings);
}
