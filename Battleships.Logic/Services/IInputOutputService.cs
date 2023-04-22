namespace Battleships.Logic.Services;

public interface IInputOutputService
{
    void WriteLine(string text);

    string? ReadLine();
}
