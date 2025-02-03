using PacificBattle.Classes;
using PacificBattle.Data;
using PacificBattle.Data.ContextModels;
using PacificBattle.Models;
using Serilog;

namespace PacificBattle.Managers
{
    public class FleetManager
    {
        private readonly AppDbContext _db = default!;
        private readonly List<Ship> allShips = [];

        public List<CombatShip> ActiveShips { get; set; } = [];

        public FleetManager(AppDbContext db)
        {
            _db = db;
            allShips = [.. _db.Ships];
        }

        public void ChangeTurns(int turn)
        {
            RemoveShipsByTurn(turn);
            var arrivingShips = allShips.Where(x => x.Turn == turn).ToList();
            BuildCombatShips(arrivingShips);
        }

        public void RemoveShipsByTurn(int turn)
        {
            foreach (var ship in ActiveShips)
            {
                if (ship.EndTurn == turn)
                {
                    ActiveShips.Remove(ship);
                    Log.Information("{ship} Removed.", ship.ShipName);
                }
            }
        }

        private void BuildCombatShips(List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                ActiveShips.Add(Shipyard.BuildShip(ship));
            }
        }                
    }
}
