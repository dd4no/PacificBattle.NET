using Microsoft.AspNetCore.Components;
using PacificBattle.CombatResolution;
using PacificBattle.Interfaces;
using PacificBattle.Ships;

namespace PacificBattle.Components.Pages
{
    public partial class BattleBox
    {
        [Inject] public IFleetManager FM { get; set; } = default!;

        public List<CombatShip> Aggressors { get; set; } = [];
        public List<CombatShip> Defenders { get; set; } = [];
        public List<string> BattleLogs { get; set; } = [];

        private readonly SelectionCoordinator _coordinator = new();

        private bool _isRoundComplete;
        private bool _inSelectionMode;
        private bool _isFirstRound;
        private bool _isNewOrder;

        #region Init
        protected override void OnInitialized()
        {
            GetShips();
        }

        private void GetShips()
        {
            Aggressors = FM.GetRandomFleetByNavy(2, 3);
            Defenders = FM.GetRandomFleetByNavy(1, 4);
            _isRoundComplete = false;
            _isFirstRound = true;
            _inSelectionMode = false;
            BattleLogs.Add("Ships enter sea area");
            BattleLogs.Add("Japan attacks first");
            BattleLogs.Add("-------------------");
        }

        private void Reset()
        {
            Aggressors.Clear();
            Defenders.Clear();
            BattleLogs.Clear();
        }

        private string GetShipCss(CombatShip ship)
        {
            var css = string.Empty;

            if (_inSelectionMode)
            {
                css += " selectable";
            }

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
            _inSelectionMode = true;
            _isNewOrder = true;
            BattleLogs.Add("Selecting targets...");
        }

        private void SelectShip(CombatShip ship)
        {
            if (_inSelectionMode)
            {
                // Issue a new Order
                if (_isNewOrder)
                {
                    // Prevent reselection
                    if (ship.HasAttackOrder)
                    {
                        BattleLogs.Add("Ship already selected");
                    }
                    else
                    {
                        // Prevent Defender from being selected
                        if (Defenders.Contains(ship))
                        {
                            BattleLogs.Add("Choose one of your ships!");
                        }
                        else
                        {
                            // Issue Order
                            _coordinator.IssueAttackOrder(ship);
                            _isNewOrder = !_coordinator.IsPairing;
                        }
                    }
                }
                // Select Target
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
                        // Issue Order
                        _coordinator.IssueAttackOrder(ship);
                        BattleLogs.Add(_coordinator.Message);
                        _isNewOrder = !_coordinator.IsPairing;
                    }
                }
            }
        }

        private void ResolveCombat()
        {
            BattleLogs.Add("-------------------");
            if (_isFirstRound)
            {
                StartRoundTwo();
                _isFirstRound = false;
            }
            else
            {
                BattleLogs.Add("Round Over");
                Reset();
                _isRoundComplete = true;
                _inSelectionMode = false;
            }
        }

        private void ClearOrders()
        {
            _coordinator.ClearOrders();
            BattleLogs.Add(_coordinator.Message);
            BattleLogs.Add("-------------------");
        }

        public void StartRoundTwo()
        {
            // Clear Orders
            ClearOrders();

            // Swap groups
            (Defenders, Aggressors) = (Aggressors, Defenders);

            // Exit Selection Mode
            _inSelectionMode = false;
            BattleLogs.Add("Allies return fire");
            BattleLogs.Add("-------------------");
        }
    }
}