using PacificBattle.CombatResolution;
using PacificBattle.Ships;

namespace PacificBattle.Components.Pages
{
    public partial class BattleBox
    {
        public List<Ships.CombatShip> Aggressors { get; set; } = [];
        public List<Ships.CombatShip> Defenders { get; set; } = [];

        public List<string> BattleLogs { get; set; } = [];

        public int Round { get; set; }

        private SelectionCoordinator _coordinator = new();
        private bool _selecting;
        private bool _startNewPair;

        #region Init
        protected override void OnInitialized()
        {
            GetShips();
        }

        private void Reset()
        {
            Aggressors.Clear();
            Defenders.Clear();
            BattleLogs.Clear();
            Round = 1;
            _startNewPair = true;
        }

        private void GetShips()
        {
            Reset();
            Aggressors = fm.GetRandomFleetByNavy(2, 3);
            Defenders = fm.GetRandomFleetByNavy(1, 4);
        }

        #endregion

        private void StartSelecting()
        {
            _selecting = true;
        }

        private void OnShipSelected(CombatShip ship)
        {
            // Start a new Pair
            if (_startNewPair)
            {
                // Prevent reselection
                if (!ship.Selected)
                {
                    // Prevent Defender from being first choice
                    if (Defenders.Contains(ship))
                    {
                        BattleLogs.Add("Choose one of your ships!");
                    }
                    else
                    {
                        _coordinator.AddToPair(ship);
                        BattleLogs.Add(_coordinator.Message);
                        _startNewPair = !_coordinator.IsPairing;
                    }
                }
                else
                {
                    BattleLogs.Add("Ship already selected");
                }
            }
            else
            {
                // Prevent Friendly Fire
                if (Aggressors.Contains(ship)
                    && !EqualityComparer<CombatShip>.Default
                    .Equals(_coordinator.SelectedShip, ship))
                {
                    BattleLogs.Add("Don't shoot your own ship!");
                }
                else
                {
                    _coordinator.AddToPair(ship);
                    BattleLogs.Add(_coordinator.Message);
                    _startNewPair = !_coordinator.IsPairing;
                }
            }
        }

        private void ResolveCombat()
        {
            _selecting = false;
        }
    }
}