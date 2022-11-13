using DevExpress.Mvvm;
using FiveLetters.UI.Models;
using FiveLetters.UI.ViewModels;
using FiveLetters.UI.Views;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace FiveLetters.UI.Services;

internal sealed class SettingsService : ISettingsService
{
    public Settings ShowDialog(Settings settings)
    {
        var settingsViewModel = new SettingsViewModel(settings);

        var view = (Window)App.ServiceProvider.GetRequiredService<SettingsView>();
        view.DataContext = settingsViewModel;
        view.ShowDialog();

        if (settingsViewModel.IsChanged(settings))
        {
            return new Settings
            {
                FilePath = new System.Uri(settingsViewModel.FilePath, System.UriKind.Absolute),
                LangMode = settingsViewModel.LangMode,
                LettersCount = settingsViewModel.WordLength
            };
        }

        return null;
    }
}
