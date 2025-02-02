namespace PacificBattle.Classes
{
    public class Location
    {
        public string LocationGroup { get; set; } = string.Empty;
        public bool InPort { get; set; } = true;
        public int PortId { get; set; }
        public int SeaId { get; set; }
    }
}
