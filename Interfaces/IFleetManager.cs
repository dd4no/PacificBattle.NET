using PacificBattle.Data.ContextModels;
using PacificBattle.Ships;

namespace PacificBattle.Interfaces
{
    public interface IFleetManager
    {
        List<Ship> GetAllShips();
        List<Ship> GetAllShipsByNavy(int navyId);
        CombatShip GetRandomShipByNavy(int navyId);
        List<CombatShip> GetFleet(int navyId, int numberOfShips);
    }
}