
namespace PacificBattle.Managers
{
    public class TurnManager
    {
        private GameManager gm;
        private FleetManager fm;
        private BaseManager bm;
        private SeaManager sm;

        public int Turn { get; private set; } = 1;
        public int POC { get; }


        public TurnManager(GameManager gmgr, FleetManager fmgr, BaseManager bmgr, SeaManager smgr)
        {
            gm = gmgr;
            fm = fmgr;
            bm = bmgr;
            sm = smgr;
        }

        public void StartTurnOne()
        {
            fm.ChangeTurns(1);
        }

        private void StartNewTurn()
        {
            Turn++;
            fm.ChangeTurns(Turn);
        }

        // activate/deactivate ships
        // tally poc

    }
}
