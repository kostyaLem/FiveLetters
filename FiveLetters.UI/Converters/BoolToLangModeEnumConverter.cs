using FiveLetters.UI.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FiveLetters.UI.Converters;

/// <summary>
/// Конвертер для выбора языка по чекбоксу на интерфейсе в настройках
/// </summary>
public class BoolToLangModeEnumConverter : MarkupExtension, IValueConverter
{
    // Вернуть true, если выбран Rus язык и Eng, если false
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var langMode = (LangMode)value;

        return langMode == LangMode.Rus ? true : false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var isChecked = (bool)value;

        return isChecked ? LangMode.Rus : LangMode.Eng;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
        => this;
}
