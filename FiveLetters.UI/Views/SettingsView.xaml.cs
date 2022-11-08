using FiveLetters.UI.ViewModels;
using HandyControl.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace FiveLetters.UI.Views;

public partial class SettingsView : Window
{
    public SettingsView()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<SettingsViewModel>();
    }

    private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        this.Close();
    }

    private void BtnSave_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        this.Close();
    }
}
