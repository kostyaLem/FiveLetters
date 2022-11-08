using System.Windows;

namespace FiveLetters.UI.Controls;

public class LetterRoutedEventArgs : RoutedEventArgs
{
    public char Letter { get; }

    public LetterRoutedEventArgs(char letter)
    {
        Letter = letter;
    }
}
