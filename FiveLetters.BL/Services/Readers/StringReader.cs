namespace FiveLetters.BL.Services.Readers;

internal static class StringReader
{
    public static async Task<IReadOnlyList<string>> ReadStrings(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentNullException(nameof(filePath));
        }

        var strs = new List<string>();
        using var stream = new StreamReader(filePath);

        while (stream.EndOfStream)
        {
            strs.Add(await stream.ReadLineAsync() ?? string.Empty);
        }

        return strs;
    }
}
