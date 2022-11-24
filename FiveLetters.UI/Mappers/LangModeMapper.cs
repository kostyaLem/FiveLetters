using System;

namespace FiveLetters.UI.Mappers;

/// <summary>
/// Маппер выбранного языка из UI в BL слой
/// </summary>
internal static class LangModeMapper
{
    public static BL.Models.Settings.LangMode Map(this Models.LangMode langMode)
        => langMode switch
        {
            Models.LangMode.Rus => BL.Models.Settings.LangMode.Rus,
            Models.LangMode.Eng => BL.Models.Settings.LangMode.Eng,
            _ => throw new ArgumentException($"Unsupported LangMode {langMode}.")
        };
}
