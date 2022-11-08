using FiveLetters.UI.ViewModels;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace FiveLetters.UI.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<MainViewModel>();
    }
}
