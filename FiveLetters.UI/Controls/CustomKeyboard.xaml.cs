using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FiveLetters.UI.Controls;

public partial class CustomKeyboard : UserControl
{
    public static readonly DependencyProperty IsRusLangProperty
        = DependencyProperty.Register("IsRusLang", typeof(bool), typeof(CustomKeyboard), new() { DefaultValue = true });

    public bool IsRusLang
    {
        get => (bool)GetValue(IsRusLangProperty);
        set => SetValue(IsRusLangProperty, value);
    }

    public CustomKeyboard()
    {
        InitializeComponent();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        var letters = GetLetters();

        Rows.ItemsSource = letters;
    }

    private char[][] GetLetters()
    {
        if (IsRusLang)
        {
            return new char[][]
            {
                "ЙЦУКЕЁНГШЩЗХЪ".Select(x=>x).ToArray(),
                "ФЫВАПРОЛДЖЭ".Select(x=>x).ToArray(),
                "ЯЧСМИТЬБЮ".Select(x=>x).ToArray()
            };
        }
        else
        {
            return new char[][]
            {
                "QWERTYUIOP".Select(x=>x).ToArray(),
                "ASDFGHJKL".Select(x=>x).ToArray(),
                "ZXCVBNM".Select(x=>x).ToArray()
            };
        }
    }
}
