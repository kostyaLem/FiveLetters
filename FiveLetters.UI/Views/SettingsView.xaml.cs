using HandyControl.Controls;

namespace FiveLetters.UI.Views;

public partial class SettingsView : Window
{
    public SettingsView()
    {
        InitializeComponent();
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
