using FiveLetters.BL.Services;
using FiveLetters.BL.Services.Readers;
using FiveLetters.UI.Models;
using FiveLetters.UI.Services;
using FiveLetters.UI.Services.Interfaces;
using FiveLetters.UI.ViewModels;
using FiveLetters.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace FiveLetters.UI;

/// <summary>
/// Класс для регистрации типов в программе
/// </summary>
public static class Configuration
{
    // Регистрация сервисов для автоматического создания в контейнере
    public static void SetupServices(this IServiceCollection serviceSollection)
    {
        serviceSollection.AddSingleton<MainViewModel>();
        serviceSollection.AddSingleton<SettingsViewModel>();

        serviceSollection.AddSingleton<MainView>();
        serviceSollection.AddSingleton<WordsManager>();
        serviceSollection.AddSingleton<WordReader>();  
        serviceSollection.AddTransient<SettingsView>();

        serviceSollection.AddTransient<ISettingsService, SettingsService>();
        serviceSollection.AddTransient<IGameProcessor, GameProcessor>();

        serviceSollection.AddSingleton(new Settings
        {
            FilePath = new Uri(Path.Combine(Environment.CurrentDirectory, "Resources\\RussianWords.txt"), UriKind.Absolute),
            LettersCount = 5,
            LangMode = LangMode.Rus
        });
    }
}
