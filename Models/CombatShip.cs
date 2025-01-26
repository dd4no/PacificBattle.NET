using PacificBattle.Classes;

namespace PacificBattle.Models
{
    public class CombatShip
    {
        public string ShipName { get; set; } = string.Empty;

        public int Attack { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }

        public Damage Damage { get; set; } = new();
        public bool IsDestroyed => Damage.TotalDamage >= Armor;

    }
}
