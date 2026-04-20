using PacificBattle.Ships;

namespace PacificBattle.CombatResolution
{
    public static class CombatManager
    {
        public static CombatShip Aggressor { get; set; } = new();
        public static CombatShip Defender { get; set; } = new();
        public static List<string> BattleLogs { get; set; } = [];

        public static void ResolveShipAttack()
        {
            if (Aggressor.Guns != 0)
            {
                BattleLogs.Add($"{Aggressor.ShipName} attacks with {Aggressor.Guns} gun(s)");
                Attack(Aggressor.Guns, Defender);
                DetermineStatus(Defender);
            }
            else
            {
                BattleLogs.Add("Aircraft carrier has no guns to attack with.");
            }

            BattleLogs.Add("Action complete");

        }

        private static void Attack(int guns, CombatShip target)
        {
            AttackCoordinator.Target = target;
            AttackCoordinator.ResolveAttack(guns);
        }

        private static void DetermineStatus(CombatShip target)
        {
            // Check for Disabled
            if (target.Damage.IsDisabled)
            {
                BattleLogs.Add($"{target.ShipName} has been disabled!");
            }

            // Log Damage
            foreach (var log in target.Damage.DamageLogs)
            {
                BattleLogs.Add(log);
            }

            // Check for Sunk
            if (target.IsSunk)
            {
                BattleLogs.Add($"{target.ShipName} has been sunk!");
            }            
        }
    }
}
