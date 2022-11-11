using FiveLetters.BL.Services;
using FiveLetters.BL.Services.Readers;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services;
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
        serviceSollection.AddSingleton<GameProcessor>(); // перенести в BL
        serviceSollection.AddSingleton<WordReader>(); // перенести в BL
        serviceSollection.AddTransient<SettingsView>();

        serviceSollection.AddSingleton<IDialogService>(x =>
        {
            var map = new Dictionary<Type, Type>()
            {
                { typeof(SettingsViewModel), typeof(SettingsView) }
            };

            return new DialogService(map);
        });

        serviceSollection.AddSingleton(new Settings
        {
            FilePath = new Uri(Path.Combine(Environment.CurrentDirectory, "RussianWords.txt"), UriKind.Absolute),
            WordLength = 5,
            LangMode = LangMode.Rus
        });
    }
}
