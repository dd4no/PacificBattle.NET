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
            Log.Information("New Damage Roll for {hits} hits", hits);

            int damage = 0;
            for (int i = 0; i < hits; i++)
            {
                damage += random.Next(1, 7);
            }
            Log.Information("{damage} damage", damage);

            return damage;
        }
    }
}
