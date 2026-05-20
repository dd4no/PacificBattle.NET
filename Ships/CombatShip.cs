namespace PacificBattle.Ships
{
    public class CombatShip
    {
        public string ShipName { get; set; } = string.Empty;

        public int NavyId { get; set; }
        public string Side { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int EndTurn { get; set; }


        public int Guns { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
        public int Airstrike { get; set; }
        public bool HasAttackBonus { get; set; }

        public ShipDamage Damage { get; set; } = new();
        public bool IsSunk => Damage.TotalDamage >= Armor;

        public ShipLocation Location { get; set; } = new();

        public bool Selected { get; set; }
    }
}
