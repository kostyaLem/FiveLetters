namespace FiveLetters.UI.Models;

/// <summary>
/// Статус ячейки с символом
/// </summary>
internal enum CellStyle
{
    // Буква не введена
    Empty,
    // Неправильная буква
    Wrong,
    // Буква правильная, но не на своём месте
    Nearly,
    // Буква на своём месте
    Guessed
}
