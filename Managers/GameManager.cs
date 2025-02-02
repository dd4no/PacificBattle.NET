
using PacificBattle.Models;

namespace PacificBattle.Managers
{
    public class GameManager
    {
        private FleetManager fleetManager;
        private TurnManager turnManager;
        private SeaManager seaManager;

        public int Turn { get; private set; }
        public int POC { get; private set; }
        public string WinningSide { get; private set; } = string.Empty;
        public List<CombatShip> ActiveShips { get; private set; } = [];

        public GameManager(FleetManager fm, TurnManager tm, SeaManager sm)
        {
            fleetManager = fm;
            turnManager = tm;
            seaManager = sm;
        }

        public void StartGame()
        {
            Turn = 1;
            ActiveShips.Clear();
            ActiveShips = fleetManager.ActiveShips;
            POC = 0;
            WinningSide = string.Empty;
            turnManager.StartTurnOne();
        }

        // initiate turn change
        // keep track of score
        // monitor end game condition
    }
}
