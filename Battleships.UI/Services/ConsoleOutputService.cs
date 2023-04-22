namespace Battleships.UI.Services;

using Battleships.Logic.Services;

public class ConsoleOutputService : IInputOutputService
{
    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}