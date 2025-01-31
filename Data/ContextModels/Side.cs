using System.ComponentModel.DataAnnotations.Schema;

namespace PacificBattle.Data.ContextModels
{
    public class Side
    {
        public int SideId { get; set; }

        [Column("side")]
        public string SideName { get; set; } = string.Empty;
    }
}
