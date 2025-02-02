namespace PacificBattle.Managers
{
    public class TurnManager
    {
        private GameManager gm;
        private FleetManager fleetManager;

        public int Turn { get; private set; } = 1;
        public int POC { get; }

        public TurnManager(GameManager gmgr, FleetManager fmgr)
        {
            gm = gmgr;
            fleetManager = fmgr;
        }

        public void StartTurnOne()
        {
            fleetManager.ChangeTurns(1);
        }

        private void StartNewTurn()
        {
            Turn++;
            fleetManager.ChangeTurns(Turn);
        }

        // activate/deactivate ships
        // tally poc

    }
}
