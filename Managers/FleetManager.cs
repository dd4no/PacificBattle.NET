using PacificBattle.Data;
using PacificBattle.Data.ContextModels;
using PacificBattle.Interfaces;
using PacificBattle.Ships;

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

        public List<Ship> GetAllShipsByNavy(int navyId)
        {
            var ships = _db.Ships.Where(x => x.NavyId == navyId).ToList();
            return ships;
        }

        // Get Random Ships for single battle testing
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

        #region Shipyard
        private CombatShip BuildShip(Ship ship)
        {
            try
            {
                // Build Ship
                var newShip = new CombatShip
                {
                    NavyId = ship.NavyId,
                    EndTurn = ship.EndTurn,
                    ShipName = ship.ShipName,
                    Guns = ship.Attack,
                    Armor = ship.Armor,
                    Speed = ship.Speed,
                    Airstrike = ship.Airstrike,
                    HasAttackBonus = ship.HasAttackBonus,
                    Damage = new(),
                    Location = new()
                };

                // Assign Side
                if (ship.NavyId == 2)
                {
                    newShip.Side = "Japan";
                }
                else
                {
                    newShip.Side = "Allies";
                }

                // Assign Starting Location
                if (ship.LocationGroup != "A")
                {
                    newShip.Location.LocationGroup = ship.LocationGroup;
                }

                // Assign Type
                if (ship.Airstrike > 0)
                {
                    newShip.Type = "Aircraft Carrier";
                }
                else if (ship.Attack > 1 && ship.Airstrike == 0)
                {
                    newShip.Type = "Battleship";
                }
                else
                {
                    newShip.Type = "Cruiser";
                }

                return newShip;
            }
            catch (Exception ex)
            {
                _logger.LogError("{ex}", ex);
                return new();
            }
        }        
        #endregion Shipyard
    }
}
