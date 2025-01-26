using PacificBattle.Models;
using Serilog;

namespace PacificBattle.Classes
{
    public class Attacker
    {
        private readonly Roller roller = new();

        public required CombatShip Target { get; set; }

        public List<int> Results { get; set; } = [];


        public void ResolveAttack(int attack)
        {
            Log.Information("Resolving attack");
            Results.Clear();
            Results = roller.Roll(attack);
            Target.Damage.CheckForDisabled(Results);
            Target.Damage.CalculateDamage(Results);
            Log.Information("Attack complete");
        }
    }
}
