namespace Battleships.Logic.Services;

public class CoordinatesService
{
    public (int X, int Y) FromStringToCoordinates(string coordinates)
    {
        if (string.IsNullOrWhiteSpace(coordinates))
        {
            throw new ArgumentException("Coordinates cannot be null or white space");
        }

        if (coordinates.Length < 2)
        {
            throw new ArgumentException("Provided string was too short");
        }

        int y;
        if (!int.TryParse(coordinates.Substring(1), out y))
        {
            throw new ArgumentException("Expected a number after a letter");
        }

        y--;
        int x = GetIndexInAlphabet(coordinates[0]);

        return (x, y);
    }

    public string FromCoordinatesToString(int x, int y)
    {
        var letter = FromNumberToLetter(x);
        var number = FromOBasedIndexTo1Based(y);

        return $"{letter}{number}";
    }

    public char FromNumberToLetter(int n)
    {
        if (n > 25 || n < 0)
        {
            throw new ArgumentOutOfRangeException("n", "Argument for this method must be between 0 and 25");
        }

        return (char)('A' + n);
    }

    public int FromOBasedIndexTo1Based(int n)
    {
        return n + 1;
    }

    private int GetIndexInAlphabet(char value)
    {
        char upper = char.ToUpper(value);
        if (upper < 'A' || upper > 'Z')
        {
            throw new ArgumentException("First character should be standard Latin character");
        }

        return (int)upper - (int)'A';
    }
}
