namespace Battleships.Logic.Models;

using System.Text;
using Battleships.Logic.Services;

public class GameGrid
{
    private readonly IInputOutputService ioService;

    private readonly CoordinatesService coordinatesService;

    private Random random;

    private GameParameters gameParameters;

    public GameGrid(IInputOutputService ioService, GameParameters gameParameters, CoordinatesService coordinatesService, Random? random = null)
    {
        this.ioService = ioService;
        this.coordinatesService = coordinatesService;
        BoardSize = gameParameters.BoardSize;

        Cells = new Cell[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                Cells[i, j] = new Cell(true, false, false, 0);
            }
        }

        this.random = random ?? new Random();
        this.gameParameters = gameParameters;
        GenerateShips();
    }

    public Cell[,] Cells { get; }

    public int RemainingPartsOfShip { get; private set; }

    public int BoardSize { get; }

    public void Print()
    {
        var board = GenerateCurrentBoardView();
        ioService.WriteLine(board);
    }

    public string GenerateCurrentBoardView()
    {
        var board = new StringBuilder("  ");
        for (int i = 0; i < BoardSize; i++)
        {
            board.Append(coordinatesService.FromNumberToLetter(i));
        }

        board.Append(Environment.NewLine);
        for (int i = 0; i < BoardSize; i++)
        {
            board.Append(PrepareNumberToDisplay(i));
            for (int j = 0; j < BoardSize; j++)
            {
                if (Cells[j, j].WasShot)
                {
                    board.Append("x");
                }
                else
                {
                    board.Append(Cells[j, i].IsEmpty ? "_" : "o");
                }
            }

            board.Append(Environment.NewLine);
        }

        return board.ToString();
    }

    public void DecreaseRemainingParts()
    {
        if (RemainingPartsOfShip > 0)
        {
            RemainingPartsOfShip--;
        }
    }

    private string PrepareNumberToDisplay(int i)
    {
        var j = coordinatesService.FromOBasedIndexTo1Based(i);
        return j < 10 ? " " + j : j.ToString();
    }

    private void GenerateShips()
    {
        foreach (ShipType shipType in Enum.GetValues(typeof(ShipType)))
        {
            var count = gameParameters.GetInitialCountByType(shipType);
            var size = gameParameters.GetInitialSizeByType(shipType);

            for (int i = 0; i < count; i++)
            {
                RemainingPartsOfShip += size;
                bool generateVertical = random.NextDouble() < 0.5;
                int x = 0;
                int y = 0;
                Direction direction = Direction.Vertical;

                if (!generateVertical)
                {
                    direction = Direction.Horizontal;
                }

                do
                {
                    if (generateVertical)
                    {
                        x = random.Next(0, BoardSize);
                        y = random.Next(0, BoardSize - size + 1);

                        direction = Direction.Vertical;
                    }

                    if (!generateVertical)
                    {
                        x = random.Next(0, BoardSize - size + 1);
                        y = random.Next(0, BoardSize);

                        direction = Direction.Horizontal;
                    }
                }
                while (!IsLocationAvailableForNewShip(x, y, size, direction));

                PutNewShipAtLocation(x, y, size, direction);
            }
        }
    }

    private bool IsLocationAvailableForNewShip(int x, int y, int size, Direction direction)
    {
        if (!Cells[x, y].IsEmpty)
        {
            return false;
        }

        if (direction == Direction.Vertical)
        {
            for (int i = 0; i < size; i++)
            {
                if (Cells[x, y + i].IsNeighbourOfShip)
                {
                    return false;
                }
            }
        }

        if (direction == Direction.Horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                if (Cells[x + i, y].IsNeighbourOfShip)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void PutNewShipAtLocation(int x, int y, int size, Direction direction)
    {
        if (direction == Direction.Vertical)
        {
            for (int i = 0; i < size; i++)
            {
                Cells[x, y + i].IsEmpty = false;
                Cells[x, y + i].SizeOfParentShip = size;
            }

            for (int i = -1; i < size + 1; i++)
            {
                SetNeighbourOfShip(x - 1, y + i);
                SetNeighbourOfShip(x + 1, y + i);
            }

            SetNeighbourOfShip(x, y - 1);
            SetNeighbourOfShip(x, y + size);
        }

        if (direction == Direction.Horizontal)
        {
            for (int i = 0; i < size; i++)
            {
                Cells[x + i, y].IsEmpty = false;
                Cells[x + i, y].SizeOfParentShip = size;
            }

            for (int i = -1; i < size + 1; i++)
            {
                SetNeighbourOfShip(x + i, y - 1);
                SetNeighbourOfShip(x + i, y + 1);
            }

            SetNeighbourOfShip(x - 1, y);
            SetNeighbourOfShip(x + size, y);
        }
    }

    private void SetNeighbourOfShip(int x, int y)
    {
        if (x < 0 || x >= BoardSize)
        {
            return;
        }

        if (y < 0 || y >= BoardSize)
        {
            return;
        }

        Cells[x, y].IsNeighbourOfShip = true;
    }
}