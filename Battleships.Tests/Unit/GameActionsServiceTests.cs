namespace Battleships.Tests.Unit;

using Battleships.Logic.Models;
using Battleships.Logic.Services;
using Battleships.Tests.Models;
using Moq;

public class GameActionsServiceTests
{
    private readonly GameActionsService gameActionsService;

    public GameActionsServiceTests()
    {
        var gameParameters = new TestGameParameters();
        var coordinatesService = new CoordinatesService();
        var ioServiceMock = new Mock<IInputOutputService>();
        gameActionsService = new GameActionsService(ioServiceMock.Object, gameParameters, coordinatesService, new Random(1));
    }

    [Fact]
    public void WhenPlayerProvidesNotExistingCoordinatesNothingIsDestroyed()
    {
        var shotResult = gameActionsService.ShootByPlayer(5, 5);

        Assert.Equal(ShotResult.Miss, shotResult);
    }

    [Fact]
    public void WhenPlayerHitsShipPartIsDestructed()
    {
        var shotResult = gameActionsService.ShootByPlayer(0, 0);

        Assert.Equal(ShotResult.Hit, shotResult);
    }

    [Fact]
    public void WhenPartIsShotTwiceByPlayerSecondAttemptIsMiss()
    {
        gameActionsService.ShootByPlayer(0, 0);
        var shotResult = gameActionsService.ShootByPlayer(0, 0);

        Assert.Equal(ShotResult.Miss, shotResult);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(0, 1)]
    public void WhenPlayerMissessShipPartIsNotDestructed(int x, int y)
    {
        var shotResult = gameActionsService.ShootByPlayer(x, y);

        Assert.Equal(ShotResult.Miss, shotResult);
    }

    [Fact]
    public void WhenPlayerDestructsAllShipsGameIsOver()
    {
        DestructAllOpponentShips(gameActionsService);

        Assert.True(gameActionsService.IsOver());
    }

    [Fact]
    public void WhenPlayerDestructsAllShipsOponentHasLost()
    {
        DestructAllOpponentShips(gameActionsService);

        Assert.True(gameActionsService.HasOponentLost());
    }

    [Fact]
    public void WhenPlayerDestructsShipLastShotIsSink()
    {
        var shotResults = DestructAllOpponentShips(gameActionsService);

        Assert.Equal(ShotResult.Hit, shotResults[0]);
        Assert.Equal(ShotResult.Sink, shotResults[1]);
    }

    [Fact]
    public void WhenOpponentProvidesNotExistingCoordinatesNothingIsDestroyed()
    {
        var shotResult = gameActionsService.ShootByOpponent(5, 5);

        Assert.Equal(ShotResult.Miss, shotResult);
    }

    [Fact]
    public void WhenOpponentHitsShipPartIsDestructed()
    {
        var shotResult = gameActionsService.ShootByOpponent(0, 0);

        Assert.Equal(ShotResult.Hit, shotResult);
    }

    [Fact]
    public void WhenPartIsShotTwiceByOpponentSecondAttemptIsMiss()
    {
        gameActionsService.ShootByOpponent(0, 0);
        var shotResult = gameActionsService.ShootByOpponent(0, 0);

        Assert.Equal(ShotResult.Miss, shotResult);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    public void WhenOpponentMissessShipPartIsNotDestructed(int x, int y)
    {
        var shotResult = gameActionsService.ShootByOpponent(x, y);

        Assert.Equal(ShotResult.Miss, shotResult);
    }

    [Fact]
    public void WhenOpponentDestructsAllShipsGameIsOver()
    {
        DestructAllPlayerShips(gameActionsService);

        Assert.True(gameActionsService.IsOver());
    }

    [Fact]
    public void WhenOpponentDestructsAllShipsPlayerHasLost()
    {
        DestructAllPlayerShips(gameActionsService);

        Assert.True(gameActionsService.HasPlayerLost());
    }

    [Fact]
    public void WhenOpponentDestructsShipLastShotIsSink()
    {
        var shotResults = DestructAllPlayerShips(gameActionsService);

        Assert.Equal(ShotResult.Hit, shotResults[0]);
        Assert.Equal(ShotResult.Sink, shotResults[1]);
    }

    private static ShotResult[] DestructAllPlayerShips(GameActionsService gameActionsService)
    {
        var result1 = gameActionsService.ShootByOpponent(0, 0);
        var result2 = gameActionsService.ShootByOpponent(0, 1);

        return new ShotResult[2] { result1, result2 };
    }

    private static ShotResult[] DestructAllOpponentShips(GameActionsService gameActionsService)
    {
        var result1 = gameActionsService.ShootByPlayer(0, 0);
        var result2 = gameActionsService.ShootByPlayer(1, 0);

        return new ShotResult[2] { result1, result2 };
    }
}