using PacificBattle.Models;

namespace PacificBattle.Classes
{
    public class AttackCoordinator
    {
        private readonly Roller roller = new();

        public required CombatShip Target { get; set; }

        public List<int> Results { get; set; } = [];


        public void ResolveAttack(int numberOfAttacks)
        {
            Results.Clear();
            Results = roller.Roll(numberOfAttacks);
            Target.Damage.CalculateDamage(Results);
        }
    }
}
