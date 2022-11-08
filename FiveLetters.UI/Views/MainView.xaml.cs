using FiveLetters.UI.ViewModels;
using HandyControl.Controls;

namespace FiveLetters.UI.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
