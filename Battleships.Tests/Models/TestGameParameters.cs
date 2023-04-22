namespace Battleships.Tests.Models;

using Battleships.Logic.Models;

public class TestGameParameters : GameParameters
{
    public override int BoardSize => 2;

    public override int GetInitialCountByType(ShipType type)
    {
        return type == ShipType.Battleship ? 1 : 0;
    }

    public override int GetInitialSizeByType(ShipType type)
    {
        return type == ShipType.Battleship ? 2 : 0;
    }
}
