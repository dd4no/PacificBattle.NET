using Serilog;

namespace PacificBattle.Classes
{
    // Individual results into List
    public static class AttackRoller
    {
        private static readonly Random random = new();

        public static List<int> Roll(int guns)
        {
            Log.Information("");
            Log.Information("New Roll with {guns} guns", guns);

            List<int> results = [];
            for (int i = 0; i < guns; i++)
            {
                results.Add(random.Next(1, 7));
            }
            Log.Information("");

            return results;
        }
    }
}
