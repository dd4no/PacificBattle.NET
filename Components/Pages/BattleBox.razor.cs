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

        private readonly SelectionCoordinator _coordinator = new();
        private bool _selecting;
        private bool _startNewPair;
        private bool _firstRound;

        #region Init
        protected override void OnInitialized()
        {
            Reset();
            GetShips();
        }

        private void Reset()
        {
            Logger.LogInformation("Ships entering sea area");
            Logger.LogInformation("Japan attacks first");

            Aggressors.Clear();
            Defenders.Clear();
            BattleLogs.Clear();
            _firstRound = true;
            _startNewPair = true;
        }

        private void GetShips()
        {
            Aggressors = FM.GetRandomFleetByNavy(2, 3);
            Defenders = FM.GetRandomFleetByNavy(1, 4);
        }

        private string GetShipCss(CombatShip ship)
        {
            var css = string.Empty;

            if (_coordinator.PendingAttacker == ship)
            {
                css += " selecting";
            }

            if (ship.HasAttackOrder)
            {
                css += " assigned";
            }

            if (ship.IncomingAttackCount > 0)
            {
                css += " targeted";
            }

            return css;
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
                if (!ship.HasAttackOrder)
                {
                    // Prevent Defender from being first choice
                    if (Defenders.Contains(ship))
                    {
                        BattleLogs.Add("Choose one of your ships!");
                    }
                    else
                    {
                        _coordinator.GiveAttackOrder(ship);
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
                    .Equals(_coordinator.PendingAttacker, ship))
                {
                    BattleLogs.Add("Don't shoot your own ship!");
                }
                else
                {
                    _coordinator.GiveAttackOrder(ship);
                    BattleLogs.Add(_coordinator.Message);
                    _startNewPair = !_coordinator.IsPairing;
                }
            }
        }

        private void ResetPairings()
        {
            _startNewPair = true;
            _coordinator.ClearOrders();
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
                Reset();
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