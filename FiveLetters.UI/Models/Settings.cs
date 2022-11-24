using DevExpress.Mvvm;
using System;

namespace FiveLetters.UI.Models;

/// <summary>
/// Модель настроек игры
/// </summary>
internal record Settings
{
    // Путь до файла со словами
    public Uri FilePath { get; init; }
    // Кол-во букв в словах
    public int LettersCount { get; init; }
    // Язык слов
    public LangMode LangMode { get; init; }
}
