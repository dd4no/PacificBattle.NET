using PacificBattle.Classes;
using PacificBattle.Data.ContextModels;

namespace PacificBattle.Models
{
    public class CombatShip
    {
        public int NavyId { get; set; }
        public int EndTurn { get; set; }

        public string ShipName { get; set; } = string.Empty;

        public int Attack { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
        public int Airstrike { get; set; }
        public bool HasAttackBonus { get; set; }

        public Damage Damage { get; set; } = new();
        public bool IsDestroyed => Damage.TotalDamage >= Armor;

        public Location Location { get; set; } = new();
    }
}
