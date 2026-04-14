namespace PacificBattle.Classes
{
    public static class Roller
    {
        public static List<int> FireGuns(int guns)
        {
            List<int> results = [];
            for (int i = 0; i < guns; i++)
            {
                var roll = new Random().Next(1,7);
                results.Add(roll);
            }

            return results;
        }

        public static int RollDamage(int hits)
        {
            int totalDamage = 0;
            for (int i = 0; i < hits; i++)
            {
                var damageTaken = new Random().Next(1, 7);
                totalDamage += damageTaken;
            }

            return totalDamage;
        }
    }
}
