using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Models;

namespace ConsoleApp.Extensions
{
    public static class PassengerExtensions
    {
        public static string ToListString(this IEnumerable<Passenger> passengers)
        {
            return string.Join("\n", passengers.Select(p => $"{p.Seat}: {p.Name}"));
        }
    }
}

