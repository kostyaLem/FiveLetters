using FiveLetters.BL.Models.Settings;
using FiveLetters.UI.Models;

namespace FiveLetters.UI.Mappers;

internal static class SettingsMapper
{
    public static WordReaderSettings Map(this Settings settings)
        => new(settings.FilePath.LocalPath, LangModeMapper.Map(settings.LangMode), settings.LettersCount);
}
