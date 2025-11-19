using PacificBattle.Data.ContextModels;
using PacificBattle.Models;

namespace PacificBattle.Classes
{
    public static class Shipyard
    {
        public static CombatShip BuildShip(Ship ship)
        {
            var newShip = new CombatShip
            {
                ShipId = ship.ShipId,
                ShipClass = DetermineShipClass(ship),
                NavyId = ship.NavyId,
                EndTurn = ship.EndTurn,
                ShipName = ship.ShipName,
                Guns = ship.Attack,
                Armor = ship.Armor,
                Airstrike = ship.Airstrike,
                HasAttackBonus = ship.HasAttackBonus,
                Damage = new(),
                Status = "Active",
                Location = new()
            };
            if (ship.LocationGroup != "A")
            {
                newShip.Location.LocationGroup = ship.LocationGroup;
            }

            return newShip;
        }

        public static List<CombatShip> ComposeFleet(List<Ship> ships)
        {
            List<CombatShip> fleet = [];
            foreach (var ship in ships)
            {
                var newship = BuildShip(ship);
                fleet.Add(newship);
            }
            return fleet;
        }

        private static string DetermineShipClass(Ship ship)
        {
            var shipClass = string.Empty;
            if (ship.Airstrike > 0)
            {
                shipClass = "Aircraft Carrier";
            }
            else if (ship.Attack > 1)
            {
                shipClass = "Battleship";
            }
            else
            {
                shipClass = "Cruiser";
            }
            return shipClass;
        }
    }
}
