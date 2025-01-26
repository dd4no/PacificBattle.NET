using Serilog;

namespace PacificBattle.Classes
{
    public class Damage
    {
        private readonly Roller roller = new();

        public int Hits { get; set; }
        public int TotalDamage { get; set; }
        public bool IsDisabled { get; set; }

        public void CheckForDisabled(List<int> results)
        {
            if (results.Contains(5))
            {
                Log.Information("Ship is disabled");
                IsDisabled = true;
            }
        }

        public void CalculateDamage(List<int> results)
        {
            Log.Information("Calculating damage");
            int damage = 0;
            int hits = 0;

            // Count Hits
            foreach (var result in results)
            {
                if (result == 6)
                {
                    Log.Information("Hit!");
                    hits++;
                }
            }

            // Add to total hits endured
            Hits += hits;
            Log.Information("Number of hits: " + hits);

            // Roll for damage
            var damageRolls = roller.Roll(hits);
            foreach (var roll in damageRolls)
            {
                Log.Information("Damage roll: " + roll);
                damage += roll;
            }

            TotalDamage += damage;
            Log.Information("Total damage added: " + damage);
            Log.Information("Total damage: " + TotalDamage);
        }
    }
}
