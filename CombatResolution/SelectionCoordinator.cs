using PacificBattle.Ships;

namespace PacificBattle.CombatResolution
{
    public class SelectionCoordinator
    {
        public List<CombatShip> Aggressors => _aggressors;
        private List<CombatShip> _aggressors = [];
        public List<CombatShip> Defenders => _defenders;
        private List<CombatShip> _defenders = [];

        public int Round { get; private set; }


        public Dictionary<CombatShip, CombatShip> Pairs { get; set; } = [];
        public bool IsPairing { get; set; }
        public string Message { get; set; } = string.Empty;

        public CombatShip? SelectedShip { get; set; }

        public bool AddToPair(CombatShip ship)
        {
            Message = string.Empty;

            // Select first item
            if (SelectedShip is null)
            {
                SelectedShip = ship;
                IsPairing = true;
                ship.Selected = true;
                Message = $"{ship.ShipName} selected";
                return true;
            }

            // Deselect same item on second click
            if (EqualityComparer<CombatShip>.Default.Equals(SelectedShip, ship))
            {
                SelectedShip = null;
                IsPairing = false;
                ship.Selected = false;
                Message = $"{ship.ShipName} deselected";
                return true;
            }

            // Add second item and add pair to dictionary
            ship.Selected = true;
            Pairs[SelectedShip] = ship;
            var pair = Pairs.Last();
            Message = $"{pair.Key.ShipName} vs. {pair.Value.ShipName}";

            // End selection round
            SelectedShip = null;
            IsPairing = false;
            return true;
        }
    }
}
