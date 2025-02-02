using PacificBattle.Models;

namespace PacificBattle.Classes
{
    public class AttackCoordinator
    {
        public required CombatShip Target { get; set; }

        public List<int> Results { get; set; } = [];


        public void ResolveAttack(int numberOfAttacks)
        {
            Results.Clear();
            Results = Roller.RollAttack(numberOfAttacks);
            Target.Damage.CalculateDamage(Results);
        }
    }
}
