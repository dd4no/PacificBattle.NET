using PacificBattle.Classes;
using PacificBattle.Data.ContextModels;
using PacificBattle.Interfaces;
using PacificBattle.Models;
using Serilog;

namespace PacificBattle.Managers
{
    public class FleetManager : IFleetManager
    {
        private readonly static Random _random = new();
        private readonly HttpClient _http;
        private List<Ship> _allShips = [];

        public List<CombatShip> ActiveShips { get; set; } = [];
        public List<CombatShip> SunkShips { get; set; } = [];

        public FleetManager(HttpClient httpClient)
        {
            _http = httpClient;
        }

        private async Task GetAllShips()
        {
            _allShips = await _http.GetFromJsonAsync<List<Ship>>("DBController/") ?? [];
        }

        public void ChangeTurns(int turn)
        {
            RemoveShipsByTurn(turn);
            var arrivingShips = _allShips.Where(x => x.Turn == turn).ToList();
            BuildCombatShips(arrivingShips);
        }

        public CombatShip BuildRandomShip(int navy)
        {
            var navalShips = _http.GetFromJsonAsync<List<Ship>>($"GetTestShipsByNavy/{navy}").Result ?? [];
            int randomIndex = _random.Next(1, navalShips.Count);
            Ship ship = navalShips[randomIndex];
            return BuildCombatShip(ship);
        }

        private CombatShip BuildCombatShip(Ship ship)
        {
            return Shipyard.BuildShip(ship);
        }


        private void BuildCombatShips(List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                ActiveShips.Add(Shipyard.BuildShip(ship));
            }
        }

        private void RemoveShipsByTurn(int turn)
        {
            foreach (var ship in ActiveShips)
            {
                if (ship.EndTurn == turn)
                {
                    ActiveShips.Remove(ship);
                    Log.Information("{ship} Removed.", ship.ShipName);
                }
            }
        }
    }
}
