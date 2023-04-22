namespace Battleships.Tests.Unit;

using Battleships.Logic.Models;
using Battleships.Logic.Services;
using Battleships.Tests.Models;
using Moq;

public class GameGridTests
{
    private readonly GameParameters gameParameters = new TestGameParameters();

    private readonly CoordinatesService coordinatesService = new CoordinatesService();

    private Mock<IInputOutputService> ioServiceMock = new Mock<IInputOutputService>();

    [Fact]
    public void WhenGameIsStartedEmptyCellsCountShouldBeCorrect()
    {
        var grid = new GameGrid(ioServiceMock.Object, gameParameters, coordinatesService);

        var emptyCellsCount = CountEmptyCells(grid.Cells);

        Assert.Equal(2, grid.RemainingPartsOfShip);
        Assert.Equal(2, emptyCellsCount);
    }

    [Fact]
    public void GenerateCurrentBoardShouldGenerateStringOfExpectedLength()
    {
        var grid = new GameGrid(ioServiceMock.Object, gameParameters, coordinatesService);

        var currentBoard = grid.GenerateCurrentBoardView();

        Assert.Equal(18, currentBoard.Length);
    }

    [Fact]
    public void GenerateCurrentBoardShouldGenerateExpectedString()
    {
        var random = new Random(1);
        var grid = new GameGrid(ioServiceMock.Object, gameParameters, coordinatesService, random);
        var nl = Environment.NewLine;

        var currentBoard = grid.GenerateCurrentBoardView();

        Assert.Equal($"  AB{nl} 1o_{nl} 2o_{nl}", currentBoard);
    }

    private int CountEmptyCells(Cell[,] grid)
    {
        var result = 0;
        foreach (var cell in grid)
        {
            if (cell.IsEmpty)
            {
                result++;
            }
        }

        return result;
    }
}