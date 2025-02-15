using Serilog;

namespace PacificBattle.Classes
{
    public static class DamageAssessor
    {
        private static int damage;
        private static int hits;
        private static bool isDisabled;
        private static List<string> combatLogs = [];

        public static DamageReport Assess(List<int> combatResults)
        {
            NewDamageReport();

            foreach (var result in combatResults)
            {
                if (result == 6)
                {
                    combatLogs.Add("Hit!");
                    hits++;
                }
                if (result == 5)
                {
                    combatLogs.Add("Disabling Hit");
                    isDisabled = true;
                    Log.Information("Disabled");
                }
                else
                {
                    combatLogs.Add("Missed");
                }
            }

            if (hits > 0)
            {
                if (hits == 1)
                {
                    combatLogs.Add($"{hits} hit.");
                    Log.Information("{hits} hit", hits);
                }
                else
                {
                    combatLogs.Add($"{hits} hits.");
                    Log.Information("{hits} hits", hits);
                }

                damage = DamageRoller.Roll(hits);
                combatLogs.Add($"{damage} taken.");
            }
            else if (!isDisabled)
            {
                combatLogs.Add("Missed");
                Log.Information("Missed");
            }

            return new DamageReport
            {
                Damage = damage,
                Hits = hits,
                IsDisabled = isDisabled,
                CombatLogs = combatLogs
            };
        }

        private static void NewDamageReport()
        {
            damage = 0;
            hits = 0;
            isDisabled = false;
            combatLogs.Clear();
        }
    }
}
