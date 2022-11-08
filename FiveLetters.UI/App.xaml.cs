using FiveLetters.UI;
using FiveLetters.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace FiveLetters;

/// <summary>
/// Класс старта программы
/// </summary>
public partial class App : Application
{
    public static readonly IServiceProvider ServiceProvider;

    static App()
    {
        ServiceProvider = CreateServiceCollection();
    }

    // Регистрация всхе зависимостей приложения для его запуска
    private static IServiceProvider CreateServiceCollection()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.SetupServices();

        return serviceCollection.BuildServiceProvider();
    }

    // Старт программы
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ServiceProvider.GetRequiredService<MainView>().ShowDialog();
    }
}
