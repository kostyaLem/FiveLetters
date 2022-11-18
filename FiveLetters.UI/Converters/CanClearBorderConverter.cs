using FiveLetters.UI.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace FiveLetters.UI.Converters;

public class CanClearBorderConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CellStyle cellStyle)
        {
            return cellStyle != CellStyle.Empty;
        }

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
        => this;
}
