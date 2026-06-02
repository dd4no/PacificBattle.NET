using PacificBattle.Ships;

namespace PacificBattle.CombatResolution
{
    public class AttackOrder
    {
        public CombatShip Attacker { get; set; } = default!;
        public CombatShip Defender { get; set; } = default!;
    }
}
