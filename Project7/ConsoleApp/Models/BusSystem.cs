using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using ConsoleApp.Extensions;

namespace ConsoleApp.Models
{
    public class BusSystem
    {
        private List<Bus> _buses;

        public BusSystem(string xmlPath)
        {
            var xml = XDocument.Load(xmlPath);
            _buses = xml.Descendants("bus")
                .Select(b => new Bus
                {
                    Id = (string)b.Attribute("id")!,
                    Route = (string)b.Element("route")!,
                    TotalSeats = (int)b.Element("totalSeats")!,
                    Passengers = b.Descendants("passenger")
                        .Select(p => new Passenger
                        {
                            Name = (string)p.Element("name")!,
                            Seat = (int)p.Element("seat")!
                        }).ToList()
                }).ToList();
        }

        public string GenerateTripReport(string id)
        {
            var bus = _buses.FirstOrDefault(b => b.Id == id);
            if (bus == null)
                throw new Exception($"Рейс с ID={id} не найден.");

            var sb = new StringBuilder();
            sb.AppendLine($"Отчёт по рейсу: {bus.Route} ({bus.Id})");
            sb.AppendLine($"Всего мест: {bus.TotalSeats}");
            sb.AppendLine($"Занято: {bus.GetOccupiedSeats()}");
            sb.AppendLine($"Свободно: {bus.GetFreeSeats()}");

            if (bus.Passengers.Any())
                sb.AppendLine("\nПассажиры:\n" + bus.Passengers.ToListString());
            else
                sb.AppendLine("\nНет зарегистрированных пассажиров.");

            return sb.ToString();
        }

        public string GenerateSummaryReport()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Сводный отчёт по всем рейсам:\n");

            foreach (var b in _buses)
                sb.AppendLine(b.GetInfo());

            var avgOccupancy = _buses.Average(b => (double)b.GetOccupiedSeats() / b.TotalSeats * 100);
            sb.AppendLine($"\nСредняя заполняемость: {avgOccupancy:F1}%");

            return sb.ToString();
        }
    }
}
