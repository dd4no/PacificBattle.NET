namespace PacificBattle.Classes
{
    public class Damage
    {
        private readonly Roller roller = new();

        public int TotalHits { get; set; }
        public int TotalDamage { get; set; }
        public bool IsDisabled { get; set; }

        public List<string> CombatResults { get; set; } = new();

        public void CalculateDamage(List<int> results)
        {
            int damage = 0;
            int hits = 0;

            foreach (var result in results)
            {
                if (result == 6)
                {
                    CombatResults.Add("Hit!");
                    hits++;
                }
                else if (result == 5)
                {
                    CombatResults.Add("Disabling Hit");
                    IsDisabled = true;
                    TotalHits++;
                }
                else
                {
                    CombatResults.Add("Miss");
                }
            }
            TotalHits += hits;
            CombatResults.Add(hits + " hits");

            // Roll for damage
            var damageRolls = roller.Roll(hits);
            foreach (var roll in damageRolls)
            {
                damage += roll;
            }

            TotalDamage += damage;
            CombatResults.Add(damage + " damage taken");
        }
    }
}
