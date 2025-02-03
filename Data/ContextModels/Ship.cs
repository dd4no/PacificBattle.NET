using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PacificBattle.Data.ContextModels
{
    public class Ship
    {
        [Key]
        public int ShipId { get; set; }
        public int NavyId { get; set; }
        [Column("name")]
        public string ShipName { get; set; } = string.Empty;
        public int Turn { get; set; }
        public int Attack { get; set; }
        public int Armor { get; set; }
        public int Speed { get; set; }
        public int Airstrike { get; set; }
        public bool HasAttackBonus { get; set; }
        public int EndTurn { get; set; }
        public string LocationGroup { get; set; } = string.Empty;
    }
}