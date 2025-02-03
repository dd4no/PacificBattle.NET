namespace PacificBattle.Managers
{
    public class SeaManager
    {
        private GameManager gm;
        private FleetManager fleetManager;
        private TurnManager turnManager;

        public SeaManager(GameManager gmgr, FleetManager fmgr, TurnManager tmgr)
        {
            gm = gmgr;
            fleetManager = fmgr;
            turnManager = tmgr;
        }

        // What Ships are in what areas

        // What bases touch what sea areas

        // which areas need resolution

        // which ships are in which ports and bases
    }
}
