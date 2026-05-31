using Microsoft.AspNetCore.Components;
using PacificBattle.CombatResolution;
using PacificBattle.Interfaces;
using PacificBattle.Ships;

namespace PacificBattle.Components.Pages
{
    public partial class BattleBox
    {
        [Inject] public IFleetManager FM { get; set; } = default!;
        [Inject] public ILogger<BattleBox> Logger { get; set; } = default!;
        public List<CombatShip> Aggressors { get; set; } = [];
        public List<CombatShip> Defenders { get; set; } = [];

        public List<string> BattleLogs { get; set; } = [];

        private bool _firstRound { get; set; }

        private readonly SelectionCoordinator _coordinator = new();
        private bool _selecting;
        private bool _startNewPair;

        #region Init
        protected override void OnInitialized()
        {
            Logger.LogInformation("Ships entering sea area");
            Logger.LogInformation("Japan attacks first");

            Aggressors.Clear();
            Defenders.Clear();
            BattleLogs.Clear();
            _firstRound = true;
            _startNewPair = true;
            GetShips();
        }

        private void GetShips()
        {
            Aggressors = FM.GetRandomFleetByNavy(2, 3);
            Defenders = FM.GetRandomFleetByNavy(1, 4);
        }

        #endregion

        private void StartSelecting()
        {
            _selecting = true;
        }

        private void SelectShip(CombatShip ship)
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

        private void ResetPairings()
        {
            _startNewPair = true;
            _coordinator.ClearPairs();
            BattleLogs.Add(_coordinator.Message);
            BattleLogs.Add("__________________");
        }

        private void ResolveCombat()
        {
            if (_firstRound)
            {
                StartRoundTwo();
            }
            else
            {
                BattleLogs.Add("Round Over");
            }
        }

        public void StartRoundTwo()
        {
            Logger.LogInformation("Allies return fire");
            _firstRound = false;
            (Defenders, Aggressors) = (Aggressors, Defenders);
            ResetPairings();
        }
    }
}