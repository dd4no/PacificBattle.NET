using PacificBattle.Models;

namespace PacificBattle.Managers
{
    public class SeaManager
    {
        public Dictionary<int, List<CombatShip>> UnitsAtSea = [];
        public Dictionary<int, List<CombatShip>> UnitsInPort = [];

        private Dictionary<int, int> seaAccess = [];
        public SeaManager()
        {
            
        }
        // What Ships are in what areas

        // What bases touch what sea areas

        // which areas need resolution

        // which ships are in which ports and bases
    }
}
