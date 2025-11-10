using System;
using System.Linq;

namespace ConsoleApp.Models
{
    public partial class Bus
    {
        public const string CompanyName = "CityTrans";
        public static readonly DateTime StartDate = DateTime.Now;

        public override int GetOccupiedSeats() => Passengers.Count;
        public override int GetFreeSeats() => TotalSeats - Passengers.Count;

        public override string GetInfo() =>
            $"{Route} | Всего: {TotalSeats}, Занято: {GetOccupiedSeats()}, Свободно: {GetFreeSeats()}";
    }
}
