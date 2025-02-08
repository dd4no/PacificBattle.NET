using Serilog;

namespace PacificBattle.Classes
{
    public static class DamageAssessor
    {
        private static int damage;
        private static int hits;
        private static bool isDisabled;
        private static List<string> combatLogs = [];

        public static DamageReport Assess(List<int> results)
        {
            NewDamageReport();

            foreach (var result in results)
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
            }

            if (hits > 0)
            {
                combatLogs.Add($"{hits} hits.");
                damage = DamageRoller.Roll(hits);
                combatLogs.Add($"{damage} taken.");
            }
            else
            {
                combatLogs.Add("Missed");
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
