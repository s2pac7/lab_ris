using System.Collections.Generic;

namespace ConsoleApp.Models
{
    public class BusBase
    {
        public string Id { get; set; } = "";
        public string Route { get; set; } = "";
        public int TotalSeats { get; set; }

        public virtual int GetOccupiedSeats() => 0;
        public virtual int GetFreeSeats() => TotalSeats;
        public virtual string GetInfo() => $"{Route} (ID: {Id})";
    }
}
