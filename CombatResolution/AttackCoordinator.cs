using PacificBattle.Classes;
using PacificBattle.Ships;

namespace PacificBattle.CombatResolution
{
    public static class AttackCoordinator
    {
        public static CombatShip Target { get; set; } = new();
        public static List<int> Results { get; set; } = [];

        public static void ResolveAttack(int guns)
        {
            Results.Clear();
            Results = Roller.FireGuns(guns);
            var damageReport = DamageAssessor.Assess(Results);
            Target.Damage.Take(damageReport);
        }
    }
}
