namespace PacificBattle.Classes
{
    public class DamageReport
    {
        public int Damage { get; set; }
        public int Hits { get; set; }
        public bool IsDisabled { get; set; }
        public List<string> CombatLogs { get; set; } = [];
    }
}
