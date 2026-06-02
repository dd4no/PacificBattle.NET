namespace PacificBattle.Ships
{
    public class CombatShip
    {
        // ID
        public string ShipName { get; set; } = string.Empty;
        public int NavyId { get; set; }
        public string Side { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        // Capabilities
        public int Guns { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
        public int Airstrike { get; set; }
        public bool HasAttackBonus { get; set; }

        // Status
        public int EndTurn { get; set; }
        public ShipDamage Damage { get; set; } = new();
        public bool IsSunk => Damage.TotalDamage >= Armor;
        public ShipLocation Location { get; set; } = new();

        // Combat Flags
        public bool HasAttackOrder { get; set; }
        public int IncomingAttackCount { get; set; }
    }
}
