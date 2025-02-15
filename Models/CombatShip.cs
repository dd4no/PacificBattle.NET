using PacificBattle.Classes;

namespace PacificBattle.Models
{
    public class CombatShip
    {
        public int ShipId { get; set; }
        public string Type { get; set; } = string.Empty;
        public int NavyId { get; set; }
        public int EndTurn { get; set; }

        public string ShipName { get; set; } = string.Empty;

        public int Guns { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
        public int Airstrike { get; set; }
        public bool HasAttackBonus { get; set; }

        public ShipDamage Damage { get; set; } = new();
        public bool IsSunk => Damage.TotalDamage >= Armor;

        public Location Location { get; set; } = new();
    }
}
