using FiveLetters.BL.Models;

namespace FiveLetters.BL.Services.Readers;

internal sealed class WordReader
{
    public async Task<IReadOnlyList<string>> ReadWords(WordReaderSettings settings)
    {
        var words = await StringReader.ReadStrings(settings.FilePath);

        var wordChecker = WordCheckerFactory.CreateChecker(settings.LangMode);

        var processedWords = words
            .Where(wordChecker.CheckWord)
            .ToList();

        return processedWords;
    }
}
