namespace Battleships.Logic.Services;

using Battleships.Logic.Models;

public class GameActionsService : IGameActionsService
{
    private readonly IInputOutputService ioService;

    private readonly CoordinatesService coordinatesService;

    private GameGrid playerGrid;
    private GameGrid oponentGrid;

    public GameActionsService(IInputOutputService ioService, GameParameters gameParameters, CoordinatesService coordinatesService, Random? random = null)
    {
        this.ioService = ioService;
        this.coordinatesService = coordinatesService;

        playerGrid = new GameGrid(ioService, gameParameters, coordinatesService, random);
        oponentGrid = new GameGrid(ioService, gameParameters, coordinatesService, random);

        DisplayGrids();
    }

    public ShotResult ShootByPlayer(int x, int y)
    {
        return Shoot(oponentGrid, x, y);
    }

    public ShotResult ShootByOpponent(int x, int y)
    {
        return Shoot(playerGrid, x, y);
    }

    public bool IsOver()
    {
        return HasPlayerLost() || HasOponentLost();
    }

    public bool HasPlayerLost()
    {
        var hasPlayerLost = playerGrid.RemainingPartsOfShip == 0;
        if (hasPlayerLost)
        {
            ioService.WriteLine("You lost");
        }

        return hasPlayerLost;
    }

    public bool HasOponentLost()
    {
        var hasOponentLost = oponentGrid.RemainingPartsOfShip == 0;
        if (hasOponentLost)
        {
            ioService.WriteLine("You won");
        }

        return hasOponentLost;
    }

    private ShotResult Shoot(GameGrid grid, int x, int y)
    {
        var cells = grid.Cells;
        if (x < 0 || x >= cells.GetLength(0))
        {
            return ShotResult.Miss;
        }

        if (y < 0 || y >= cells.GetLength(1))
        {
            return ShotResult.Miss;
        }

        var cell = cells[x, y];
        if (cell.WasShot)
        {
            // Cannot hit the same cell twice
            return ShotResult.Miss;
        }

        cell.WasShot = true;
        if (cell.IsDestroyed)
        {
            grid.DecreaseRemainingParts();
            ioService.WriteLine($"Ship that you hit has size {cell.SizeOfParentShip}");
        }

        if (cell.IsEmpty)
        {
            return ShotResult.Miss;
        }

        if (HasShipSunk(grid.Cells, x, y))
        {
            return ShotResult.Sink;
        }

        return ShotResult.Hit;
    }

    private bool HasShipSunk(Cell[,] cells, int x, int y)
    {
        if (!cells[x, y].IsDestroyed)
        {
            return false;
        }

        var potentialShipParts = GetPotentialShipParts(cells, x, y);

        foreach (var part in potentialShipParts)
        {
            if (part.IsTakenAndNotShot)
            {
                return false;
            }
        }

        return true;
    }

    private List<Cell> GetPotentialShipParts(Cell[,] cells, int x, int y)
    {
        List<Cell> parts = new List<Cell>();
        if (x - 1 > 0)
        {
            parts.Add(cells[x - 1, y]);
        }

        if (x + 1 < cells.GetLength(0))
        {
            parts.Add(cells[x + 1, y]);
        }

        if (y - 1 > 0)
        {
            parts.Add(cells[x, y - 1]);
        }

        if (y + 1 < cells.GetLength(1))
        {
            parts.Add(cells[x, y + 1]);
        }

        return parts;
    }

    private void DisplayGrids()
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Any(x => x == "-showGrids"))
        {
            playerGrid.Print();
            oponentGrid.Print();
        }
    }
}