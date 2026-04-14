namespace PacificBattle.CombatResolution
{
    public class DamageReport
    {
        public int Hits { get; set; }
        public int Damage { get; set; }
        public bool IsDisabled { get; set; }
        public List<string> CombatLogs { get; set; } = [];
    }
}
