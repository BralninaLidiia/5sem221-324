using libCore;

internal class Program
{
    private static Game g;

    private static void Main(string[] args)
    {
        Console.WriteLine("Игра устный счет");
        Console.WriteLine();

        g = new Game();
        g.ChangeQuestion += () => Console.WriteLine($"Вопрос: {g.QuestionLine}");

        var dt = DateTime.Now;
        Console.WriteLine($"Ты играл {DateTime.Now - dt} секунд");
    }
}