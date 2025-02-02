using Serilog;

namespace PacificBattle.Classes
{
    public class Roller
    {
        private static readonly Random random = new();

        public List<int> Roll(int rolls)
        {
            Log.Information("");
            Log.Information("New Roll with {rolls}", rolls);
            List<int> results = [];

            for (int i = 0; i < rolls; i++)
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
    }
}
