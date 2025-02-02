using Serilog;

namespace PacificBattle.Classes
{
    public static class Roller
    {
        private static readonly Random random = new();

        public static List<int> RollAttack(int guns)
        {
            Log.Information("");
            Log.Information("New Roll with {guns} guns", guns);

            List<int> results = [];
            for (int i = 0; i < guns; i++)
            {
                results.Add(random.Next(1, 7));
            }
            foreach (var result in results)
            {
                Log.Information(result.ToString());
            }
            Log.Information("");

            return results;
        }

        public static int RollDamage(int hits)
        {
            Log.Information("");
            Log.Information("New Damage Roll for {hits} hits", hits);

            int damageTaken = 0;
            for (int i = 0; i < hits; i++)
            {
                var damage = random.Next(1, 7);
                damageTaken += damage;
            }
            Log.Information("{damageTaken} total damage");

            return damageTaken;
        }
    }
}
