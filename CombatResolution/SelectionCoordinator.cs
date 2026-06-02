using PacificBattle.Ships;

namespace PacificBattle.CombatResolution
{
    public class SelectionCoordinator()
    {
        public List<AttackOrder> Orders { get; set; } = [];
        public CombatShip? PendingAttacker { get; set; }
        public bool IsPairing { get; set; }
        public string Message { get; set; } = string.Empty;

        public bool IssueAttackOrder(CombatShip ship)
        {
            Message = string.Empty;

            // Select ship
            if (PendingAttacker is null)
            {
                PendingAttacker = ship;
                IsPairing = true;
                return true;
            }

            // Deselect on second click
            if (EqualityComparer<CombatShip>.Default.Equals(PendingAttacker, ship))
            {
                PendingAttacker = null;
                IsPairing = false;
                return true;
            }

            // Add Attack Order
            Orders.Add( new AttackOrder()
            {
                Attacker = PendingAttacker,
                Defender = ship
            });
            PendingAttacker.HasAttackOrder = true;
            ship.IncomingAttackCount++;

            // Log Order
            var pair = Orders.Last();
            Message = $"{pair.Attacker.ShipName} attacks {pair.Defender.ShipName}";

            // End selection round
            PendingAttacker = null;
            IsPairing = false;
            return true;
        }

        public void ClearOrders()
        {
            // Ensure all flags are cleared
            if (PendingAttacker is not null)
            {
                PendingAttacker.HasAttackOrder = false;
                PendingAttacker = null;
            }
            IsPairing = false;

            // Reset selections if needed
            foreach (var order in Orders)
            {
                order.Attacker.HasAttackOrder = false;
                order.Defender.IncomingAttackCount = 0;
            }

            // Clear Orders
            Orders.Clear();
            Message = "All attack orders cleared";
        }
    }
}
