﻿using PacificBattle.Models;

namespace PacificBattle.Interfaces
{
    public interface IFleetManager
    {
        List<CombatShip> ActiveShips { get; set; }
        List<CombatShip> SunkShips { get; set; }

        CombatShip BuildRandomShipByNavy(int navy);
        void ChangeTurns(int turn);
    }
}