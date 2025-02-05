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
            damage = 0;
            hits = 0;
            isDisabled = false;
            combatLogs.Clear();

            foreach (var result in results)
            {
                if (result == 6)
                {
                    combatLogs.Add("Hit!");
                    hits++;
                }
                else if (result == 5)
                {
                    combatLogs.Add("Disabling Hit");
                    isDisabled = true;
                    Log.Information("Disabled");
                }
                else
                {
                    combatLogs.Add("Miss");
                }
            }

            combatLogs.Add($"{hits} hits.");

            damage = DamageRoller.Roll(hits);

            combatLogs.Add($"{damage} taken.");

            return new DamageReport
            {
                Damage = damage,
                Hits = hits,
                IsDisabled = isDisabled,
                CombatLogs = combatLogs
            };
        }
    }
}
