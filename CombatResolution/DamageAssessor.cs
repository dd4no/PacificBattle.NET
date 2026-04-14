using PacificBattle.Classes;

namespace PacificBattle.CombatResolution
{
    public static class DamageAssessor
    {
        private static int hits;
        private static bool isDisabled;

        private static int damage;
        private static readonly List<string> combatLogs = [];

        public static DamageReport Assess(List<int> combatResults)
        {
            ClearAndStartNewReport();

            // Tally Results
            foreach (var result in combatResults)
            {
                switch(result)
                {
                    case 6: { hits++; } break;
                    case 5: { isDisabled = true; } break;
                }
            }

            // Assess Damage
            if (isDisabled)
            {
                combatLogs.Add("Ship disabled.");
            }
            if (hits > 0)
            {
                damage = Roller.RollDamage(hits);
                combatLogs.Add($"{hits} taken for {damage} damage.");
            }
            if (hits == 0) 
            { 
                combatLogs.Add("No damage"); 
            }

            // Create Report
            var report = new DamageReport
            {
                Damage = damage,
                Hits = hits,
                IsDisabled = isDisabled,
                CombatLogs = combatLogs
            };

            return report;
        }

        // Reset Report
        private static void ClearAndStartNewReport()
        {
            damage = 0;
            hits = 0;
            isDisabled = false;
            combatLogs.Clear();
        }
    }
}
