using PacificBattle.Ships;
using Serilog;

namespace PacificBattle.CombatResolution
{
    public class SelectionCoordinator()
    {
        public List<AttackOrder> Orders { get; set; } = [];
        public CombatShip? PendingAttacker { get; set; }
        public bool IsPairing { get; set; }
        public string Message { get; set; } = string.Empty;

        public bool GiveAttackOrder(CombatShip ship)
        {
            // Select ship
            if (PendingAttacker is null)
            {
                PendingAttacker = ship;
                IsPairing = true;
                Message = $"{ship.ShipName} selected";
                return true;
            }

            // Deselect on second click
            if (EqualityComparer<CombatShip>.Default.Equals(PendingAttacker, ship))
            {
                PendingAttacker = null;
                IsPairing = false;
                Message = $"{ship.ShipName} deselected";
                return true;
            }

            // Add Attack Order
            Orders.Add( new()
            {
                Attacker = PendingAttacker,
                Defender = ship
            });
            PendingAttacker.HasAttackOrder = true;
            ship.IncomingAttackCount++;

            // Log Order
            var pair = Orders.Last();
            Message = $"{pair.Attacker.ShipName} attacks {pair.Defender.ShipName}";
            Log.Information("{msg}", Message);

            // End selection round
            PendingAttacker = null;
            IsPairing = false;
            return true;
        }

        public void ClearOrders()
        {
            if (PendingAttacker is not null)
            {
                PendingAttacker.HasAttackOrder = false;
                PendingAttacker = null;
            }

            IsPairing = false;

            foreach (var order in Orders)
            {
                order.Attacker.HasAttackOrder = false;
                order.Defender.IncomingAttackCount = 0;
            }

            Orders.Clear();

            Message = $"All match-ups reset";
            Log.Information("{msg}", Message);
        }
    }
}
