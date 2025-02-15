using Serilog;

namespace PacificBattle.Classes
{
    // Sum of rolls
    public static class DamageRoller
    {
        private static readonly Random random = new();

        public static int Roll(int hits)
        {
            Log.Information("");
            Log.Information(">>>>> Damage <<<<<");

            int totalDamage = 0;
            for (int i = 0; i < hits; i++)
            {
                var r = random.Next(1, 7);
                totalDamage += r;
                Log.Information("{r}", r);
            }
            Log.Information("total damage: {totalDamage}", totalDamage);
            Log.Information("<<<<< >>>>");

            return totalDamage;
        }
    }
}
