using Serilog;

namespace PacificBattle.Classes
{
    public class Damage
    {
        private readonly Roller roller = new();

        public int TotalHits { get; set; }
        public int TotalDamage { get; set; }
        public bool IsDisabled { get; set; }

        public List<string> CombatLog { get; set; } = new();

        public void CalculateDamage(List<int> results)
        {
            int damage = 0;
            int damageHits = 0;

            foreach (var result in results)
            {
                if (result == 6)
                {
                    CombatLog.Add("Hit!");
                    damageHits++;
                }
                else if (result == 5)
                {
                    CombatLog.Add("Disabling Hit");
                    IsDisabled = true;
                    TotalHits++;
                    Log.Information("Disabled");
                }
                else
                {
                    CombatLog.Add("Miss");
                }
            }
            TotalHits += damageHits;
            CombatLog.Add(damageHits + " damage hits");

            // Roll for damage
            var damageRolls = roller.Roll(damageHits);
            foreach (var roll in damageRolls)
            {
                damage += roll;
            }

            TotalDamage += damage;
            CombatLog.Add(damage + " damage taken");

            Log.Information("{damage} taken", damage);
            Log.Information("{damage} total", TotalDamage);
        }
    }
}
