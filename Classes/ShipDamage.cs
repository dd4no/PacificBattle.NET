using PacificBattle.CombatResolution;

namespace PacificBattle.Classes
{
    public class ShipDamage
    {
        public int LifetimeHits { get; set; }
        public int TotalDamage { get; set; }
        public bool IsDisabled { get; set; }
        public List<string> DamageLogs { get; set; } = [];

        public void Take(DamageReport report)
        {
            LifetimeHits += report.Hits;
            TotalDamage += report.Damage;
            IsDisabled = report.IsDisabled;
            foreach (var log in report.CombatLogs)
            {
                DamageLogs.Add(log);
            }
        }
    }
}
