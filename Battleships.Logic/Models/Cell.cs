namespace Battleships.Logic.Models;

public class Cell
{
    public Cell(bool isEmpty, bool isNeighbourOfShip, bool wasShot, int sizeOfParentShip)
    {
        this.IsEmpty = isEmpty;
        this.IsNeighbourOfShip = isNeighbourOfShip;
        this.WasShot = wasShot;
        this.SizeOfParentShip = sizeOfParentShip;
    }

    public bool IsEmpty { get; set; }

    public bool IsNeighbourOfShip { get; set; }

    public bool WasShot { get; set; }

    public bool IsDestroyed => WasShot && !IsEmpty;

    public bool IsTakenAndNotShot => !IsEmpty && !WasShot;

    public int SizeOfParentShip { get; set; }
}