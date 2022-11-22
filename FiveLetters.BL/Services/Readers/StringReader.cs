namespace FiveLetters.BL.Services.Readers;

/// <summary>
/// Класс для чтения строк из файла
/// </summary>
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

        // Читать строки пока файл не кончился
        while (!stream.EndOfStream)
        {
            strs.Add(await stream.ReadLineAsync() ?? string.Empty);
        }

        // Вернуть считанные строки
        return strs;
    }
}
