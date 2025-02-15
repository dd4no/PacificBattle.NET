using Serilog;

namespace PacificBattle.Classes
{
    // Individual results into List
    public static class AttackRoller
    {
        private static readonly Random random = new();

        public static List<int> Roll(int guns)
        {
            Log.Information("{guns} guns", guns);
            Log.Information("---");

            List<int> results = [];
            for (int i = 0; i < guns; i++)
            {
                results.Add(random.Next(1, 7));
            }

            foreach (var result in results)
            {
                Log.Information($"{result}");
            }
            Log.Information("---");

            return results;
        }
    }
}
