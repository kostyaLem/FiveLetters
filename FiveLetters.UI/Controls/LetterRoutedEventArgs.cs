using System.Windows;

namespace FiveLetters.UI.Controls;

/// <summary>
/// Аргументы события нажатия на букву
/// </summary>
public class LetterRoutedEventArgs : RoutedEventArgs
{
    // Выбранная буква
    public char Letter { get; }

    public LetterRoutedEventArgs(char letter)
    {
        Letter = letter;
    }
}
