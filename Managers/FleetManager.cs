using PacificBattle.Data;
using PacificBattle.Data.ContextModels;
using PacificBattle.Interfaces;
using PacificBattle.Ships;

namespace PacificBattle.Managers
{
    public class FleetManager(
        AppDbContext db, 
        ILogger<FleetManager> logger) 
        : IFleetManager
    {
        private readonly AppDbContext _db = db;
        private readonly ILogger<FleetManager> _logger = logger;

        #region Orders
        // Get All
        public List<Ship> GetAllShips()
        {
            return _db.Ships.ToList();
        }

        // Get All By Navy
        public List<Ship> GetAllShipsByNavy(int navyId)
        {
            var ships = _db.Ships.Where(x => x.NavyId == navyId).ToList();
            return ships;
        }

        // Get Random Ship for single battle testing
        public CombatShip GetRandomShipByNavy(int navyId)
        {
            var ships = GetAllShipsByNavy(navyId);
            int randomIndex = new Random().Next(1, ships.Count);
            Ship ship = ships[randomIndex];
            return BuildShip(ship);
        }

        // Get Test Fleets
        public List<CombatShip> GetRandomFleetByNavy(int navyId, int numberOfShips)
        {
            List<CombatShip> fleet = [];
            var ships = GetAllShipsByNavy(navyId);
            for (int i = 0; i < numberOfShips; i++)
            {
                int randomIndex = new Random().Next(1, ships.Count);
                fleet.Add(BuildShip(ships[randomIndex]));
            }

            return fleet;
        }
        #endregion

        #region Shipyard
        private CombatShip BuildShip(Ship ship)
        {
            try
            {
                // Build Ship
                var newShip = new CombatShip
                {
                    ShipName = ship.ShipName,
                    NavyId = ship.NavyId,
                    Side = ship.NavyId switch
                    {
                        1 => "US",
                        2 => "Japan",
                        3 => "Great Britain",
                        4 => "Austrailia",
                        5 => "Netherlands",
                        _ => "Unknown"
                    },
                    Type = GetType(ship),
                    EndTurn = ship.EndTurn,
                    Guns = ship.Attack,
                    Armor = ship.Armor,
                    Speed = ship.Speed,
                    Airstrike = ship.Airstrike,
                    HasAttackBonus = ship.HasAttackBonus,
                    Damage = new(),
                    Location = new()
                };

                // Assign Starting Location
                if (ship.LocationGroup != "A")
                {
                    newShip.Location.LocationGroup = ship.LocationGroup;
                }

                return newShip;
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
                return new();
            }
        }
        
        private static string GetType(Ship ship)
        {
            if (ship.Airstrike > 0)
            {
                return "Aircraft Carrier";
            }
            else if (ship.Attack > 1 && ship.Airstrike == 0)
            {
                return "Battleship";
            }
            else
            {
                return "Cruiser";
            }
        }
        #endregion
    }
}
