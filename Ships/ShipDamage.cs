using PacificBattle.CombatResolution;

namespace PacificBattle.Ships
{
    public class ShipDamage
    {
        public int LifetimeHits { get; private set; }
        public int TotalDamage { get; private set; }
        public bool IsDisabled { get; private set; }
        public List<string> DamageLogs { get; private set; } = [];

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
