using FiveLetters.BL.Models.Settings;

namespace FiveLetters.BL.Services;

internal static class WordCheckerFactory
{
    public static IWordChecker CreateChecker(LangMode langMode)
        => langMode switch
        {
            LangMode.Rus => new RusWordChecker(),
            LangMode.Eng => new EngWordChecker(),
            _ => throw new NotImplementedException("Неподдерживаемый язык."),
        };
}
