namespace Battleships.Logic.Services;

using Battleships.Logic.Models;

public interface IGameActionsService
{
    bool HasOponentLost();

    bool HasPlayerLost();

    bool IsOver();

    ShotResult ShootByOpponent(int x, int y);

    ShotResult ShootByPlayer(int x, int y);
}