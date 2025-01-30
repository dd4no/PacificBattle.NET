using PacificBattle.Models;
using Serilog;

namespace PacificBattle.Classes
{
    public class Attack
    {
        private readonly Roller roller = new();

        public required CombatShip Target { get; set; }

        public List<int> Results { get; set; } = [];


        public void ResolveAttack(int numberOfAttacks)
        {
            Log.Information("Resolving attack");
            Results.Clear();
            Results = roller.Roll(numberOfAttacks);
            Target.Damage.CheckForDisabled(Results);
            Target.Damage.CalculateDamage(Results);
            Log.Information("Attack complete");
        }
    }
}
