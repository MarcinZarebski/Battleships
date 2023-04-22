namespace Battleships.Tests.Unit;

using Battleships.Logic.Services;

public class CoordinatesServiceTests
{
    private readonly CoordinatesService coordinatesService;

    public CoordinatesServiceTests()
    {
        coordinatesService = new CoordinatesService();
    }

    [Theory]
    [InlineData("A1", 0, 0)]
    [InlineData("a1", 0, 0)]
    [InlineData("A2", 0, 1)]
    [InlineData("A3", 0, 2)]
    [InlineData("B1", 1, 0)]
    [InlineData("B2", 1, 1)]
    [InlineData("B3", 1, 2)]
    [InlineData("C4", 2, 3)]
    [InlineData("D4", 3, 3)]
    [InlineData("E4", 4, 3)]
    [InlineData("J10", 9, 9)]
    public void ValidStringShouldReturnExpectedCoordinates(string argument, int expectedX, int expectedY)
    {
        var (x, y) = coordinatesService.FromStringToCoordinates(argument);

        Assert.Equal(expectedX, x);
        Assert.Equal(expectedY, y);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("a")]
    [InlineData("1")]
    [InlineData("a1c")]
    public void InvalidStringShouldThrowException(string argument)
    {
        Assert.Throws<ArgumentException>(() => coordinatesService.FromStringToCoordinates(argument));
    }

    [Theory]
    [InlineData(0, 0, "A1")]
    [InlineData(0, 1, "A2")]
    [InlineData(0, 2, "A3")]
    [InlineData(1, 0, "B1")]
    [InlineData(1, 1, "B2")]
    [InlineData(1, 2, "B3")]
    [InlineData(2, 3, "C4")]
    [InlineData(3, 3, "D4")]
    [InlineData(4, 3, "E4")]
    [InlineData(9, 9, "J10")]
    public void ValidCoordinatesShouldReturnExpectedString(int x, int y, string expected)
    {
        var actual = coordinatesService.FromCoordinatesToString(x, y);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(0, 'A')]
    [InlineData(1, 'B')]
    [InlineData(2, 'C')]
    [InlineData(10, 'K')]
    [InlineData(25, 'Z')]
    public void ValidNumberShouldBeTransformedToExpectedString(int n, char letter)
    {
        var actual = coordinatesService.FromNumberToLetter(n);

        Assert.Equal(letter, actual);
    }

    [Theory]
    [InlineData(-5)]
    [InlineData(-1)]
    [InlineData(26)]
    [InlineData(100)]
    public void InValidNumberShouldThrowException(int n)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => coordinatesService.FromNumberToLetter(n));
    }
}
