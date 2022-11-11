using FiveLetters.BL.Services;

do
{
    var word = Console.ReadLine();

    var processor = new GameProcessor("слон");

    var attempt = processor.TryGuess(word);

    attempt.Letters.ToList().ForEach(x => Console.WriteLine(x));
} while (true);