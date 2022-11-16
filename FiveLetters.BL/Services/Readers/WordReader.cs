using FiveLetters.BL.Models.Settings;

namespace FiveLetters.BL.Services.Readers;

public sealed class WordReader
{
    public async Task<IReadOnlyList<string>> ReadWords(WordReaderSettings settings)
    {
        var words = await StringReader.ReadStrings(settings.FilePath);

        var wordChecker = WordCheckerFactory.CreateChecker(settings.LangMode);

        var processedWords = words
            .Select(x => x.Trim())
            .Where(x => wordChecker.CheckWord(x, settings.LettersCount))
            .Select(x => x.ToUpper())
            .ToList();

        return processedWords;
    }
}
