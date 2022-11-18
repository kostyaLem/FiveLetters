using FiveLetters.BL.Services;
using FiveLetters.BL.Services.Readers;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services;
using FiveLetters.UI.Services.Interfaces;
using FiveLetters.UI.ViewModels;
using FiveLetters.UI.Views;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;

namespace FiveLetters.UI;

public static class Configuration
{
    public static void SetupServices(this IServiceCollection serviceSollection)
    {
        serviceSollection.AddSingleton<MainViewModel>();
        serviceSollection.AddSingleton<SettingsViewModel>();

        serviceSollection.AddSingleton<MainView>();
        serviceSollection.AddSingleton<WordsManager>(); // перенести в BL
        serviceSollection.AddSingleton<WordReader>(); // перенести в BL
        serviceSollection.AddTransient<SettingsView>();

        serviceSollection.AddTransient<ISettingsService, SettingsService>();

        serviceSollection.AddSingleton(new Settings
        {
            FilePath = new Uri(Path.Combine(Environment.CurrentDirectory, "Resources\\RussianWords.txt"), UriKind.Absolute),
            LettersCount = 5,
            LangMode = LangMode.Rus
        });
    }
}
