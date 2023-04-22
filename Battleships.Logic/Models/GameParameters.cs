namespace Battleships.Logic.Models;

public abstract class GameParameters
{
    public abstract int BoardSize { get; }

    public abstract int GetInitialCountByType(ShipType type);

    public abstract int GetInitialSizeByType(ShipType type);
}
