using PacificBattle.Data.ContextModels;
using PacificBattle.Models;

namespace PacificBattle.Interfaces
{
    public interface IFleetManager
    {
        List<Ship> GetAllShips();
        CombatShip BuildRandomShipByNavy(int navyId);
    }
}