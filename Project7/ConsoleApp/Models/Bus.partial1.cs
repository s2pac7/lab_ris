using System.Collections.Generic;

namespace ConsoleApp.Models
{
    public partial class Bus : BusBase
    {
        public List<Passenger> Passengers { get; set; } = new();
    }
}
