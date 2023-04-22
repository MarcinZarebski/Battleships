namespace Battleships.Logic.Services;

using Battleships.Logic.Models;

public class GameRunnerService : IGameRunnerService
{
    private readonly IInputOutputService ioService;

    private readonly CoordinatesService coordinatesService;

    private readonly GameParameters gameParameters;

    private readonly IGameActionsService gameActionsService;

    private readonly Random random = new Random();

    public GameRunnerService(IInputOutputService ioService, CoordinatesService coordinatesService, GameParameters gameParameters, IGameActionsService gameActionsService)
    {
        this.ioService = ioService;
        this.coordinatesService = coordinatesService;
        this.gameParameters = gameParameters;
        this.gameActionsService = gameActionsService;
    }

    public void Run()
    {
        do
        {
            try
            {
                ShootAtCoordinatesFromPlayer();
                if (gameActionsService.IsOver())
                {
                    break;
                }

                ShootAtCoordinatesFromPlayer();
                if (gameActionsService.IsOver())
                {
                    break;
                }

                ShootAsOpponent();
                if (gameActionsService.IsOver())
                {
                    break;
                }

                ShootAsOpponent();
                if (gameActionsService.IsOver())
                {
                    break;
                }
            }
            catch (Exception e)
            {
                ioService.WriteLine($"Unexpected error occured: {e.Message}");
            }
        }
        while (true);
    }

    private void ShootAtCoordinatesFromPlayer()
    {
        ioService.WriteLine("Please provide coordinates e.g. a1");
        var stringCoordinates = ioService.ReadLine();
        if (stringCoordinates != null)
        {
            (var x, var y) = coordinatesService.FromStringToCoordinates(stringCoordinates);

            var result = gameActionsService.ShootByPlayer(x, y);
            ioService.WriteLine($"Result of your shot is {result}{Environment.NewLine}");
        }
    }

    private void ShootAsOpponent()
    {
        var x = GetRandomCoordinate();
        var y = GetRandomCoordinate();
        var result = gameActionsService.ShootByOpponent(x, y);
        var selectedPoint = coordinatesService.FromCoordinatesToString(x, y);
        ioService.WriteLine($"Your opponent selected {selectedPoint}{Environment.NewLine}Result of your opponent shot is {result}{Environment.NewLine}");
    }

    private int GetRandomCoordinate()
    {
        return random.Next(0, gameParameters.BoardSize);
    }
}
