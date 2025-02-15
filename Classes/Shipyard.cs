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
    }
}
