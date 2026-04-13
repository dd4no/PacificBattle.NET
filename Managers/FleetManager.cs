using PacificBattle.Data;
using PacificBattle.Data.ContextModels;
using PacificBattle.Interfaces;
using PacificBattle.Models;

namespace PacificBattle.Managers
{
    public class FleetManager(AppDbContext db, ILogger<FleetManager> logger) : IFleetManager
    {
        private readonly AppDbContext _db = db;
        private readonly ILogger<FleetManager> _logger = logger;

        // Get All
        public List<Ship> GetAllShips()
        {
            return _db.Ships.ToList();
        }

        // Get Random By Navy for testing
        public CombatShip BuildRandomShipByNavy(int navy)
        {
            try
            {
                var navalShips = _db.Ships.Where(x => x.NavyId == navy).ToList();
                int randomIndex = new Random().Next(1, navalShips.Count);
                Ship ship = navalShips[randomIndex];
                return BuildShip(ship);
            }
            catch (Exception ex)
            {
                _logger.LogError("Something went wrong. {ex}", ex);
                return new();
            }
        }

        #region Shipyard
        private static CombatShip BuildShip(Ship ship)
        {
            var newShip = new CombatShip
            {
                NavyId = ship.NavyId,
                EndTurn = ship.EndTurn,
                ShipName = ship.ShipName ?? string.Empty,
                Guns = ship.Attack,
                Armor = ship.Armor,
                Airstrike = ship.Airstrike,
                HasAttackBonus = ship.HasAttackBonus,
                Damage = new(),
                Location = new()
            };
            if (ship.LocationGroup is not null && ship.LocationGroup != "A")
            {
                newShip.Location.LocationGroup = ship.LocationGroup;
            }

            return newShip;
        }

        private static List<CombatShip> ComposeFleet(List<Ship> ships)
        {
            List<CombatShip> fleet = [];
            foreach (var ship in ships)
            {
                var newship = BuildShip(ship);
                fleet.Add(newship);
            }
            return fleet;
        }
        #endregion Shipyard
    }
}
