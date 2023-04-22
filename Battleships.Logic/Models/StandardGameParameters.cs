namespace Battleships.Logic.Models;

public class StandardGameParameters : GameParameters
{
    public override int BoardSize => 10;

    public override int GetInitialCountByType(ShipType type)
    {
        switch (type)
        {
            case ShipType.Battleship:
                return 1;

            case ShipType.Destroyer:
                return 2;

            default:
                throw new NotImplementedException($"Initial count for type {type} is not handled");
        }
    }

    public override int GetInitialSizeByType(ShipType type)
    {
        switch (type)
        {
            case ShipType.Battleship:
                return 5;

            case ShipType.Destroyer:
                return 4;

            default:
                throw new NotImplementedException($"Initial size of type {type} is not handled");
        }
    }
}
