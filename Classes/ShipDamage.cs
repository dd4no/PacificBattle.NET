using Serilog;

namespace PacificBattle.Classes
{
    public class ShipDamage
    {
        public int LifetimeHits { get; set; }
        public int TotalDamage { get; set; }
        public bool IsDisabled { get; set; }
        public List<string> DamageLogs { get; set; } = new();

        public void Take(DamageReport report)
        {
            LifetimeHits += report.Hits;
            TotalDamage += report.Damage;
            IsDisabled = report.IsDisabled;
            DamageLogs = report.CombatLogs;
        }
    }
}
