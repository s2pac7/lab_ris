using System;
using System.IO;
using ConsoleApp.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main()
        {
            // Переход на 3 уровня вверх — из bin\Debug\net8.0 в корень проекта
            string baseDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string dataDir = Path.Combine(baseDir, "Data");

            string reportsDir = Path.Combine(baseDir, "Reports");
            string logsDir = Path.Combine(baseDir, "Logs");

            // Создаём папки, если их нет
            Directory.CreateDirectory(dataDir);
            Directory.CreateDirectory(reportsDir);
            Directory.CreateDirectory(logsDir);

            Console.WriteLine("Рабочая директория: " + baseDir);
            Console.WriteLine("Папка Logs существует: " + Directory.Exists(logsDir));

            try
            {
                string xmlPath = Path.Combine(dataDir, "buses.xml");

                if (!File.Exists(xmlPath))
                {
                    Console.WriteLine($"❌ Файл {xmlPath} не найден!");
                    return;
                }

                var system = new BusSystem(xmlPath);

                Console.Write("Введите ID рейса: ");
                string id = Console.ReadLine() ?? "";

                var report = system.GenerateTripReport(id);
                File.WriteAllText(Path.Combine(reportsDir, "report_trip.txt"), report);
                Console.WriteLine("✅ Отчёт создан: report_trip.txt");

                var summary = system.GenerateSummaryReport();
                File.WriteAllText(Path.Combine(reportsDir, "report_all_trips.txt"), summary);
                Console.WriteLine("✅ Сводный отчёт создан: report_all_trips.txt");
            }
            catch (Exception ex)
            {
                // Убедимся, что перед записью логов папка точно есть
                Directory.CreateDirectory(logsDir);

                string logPath = Path.Combine(logsDir, "app.log");
                File.AppendAllText(logPath, $"[{DateTime.Now}] {ex.Message}\n");

                Console.WriteLine("⚠️ Ошибка: " + ex.Message);
                Console.WriteLine("Лог записан в: " + logPath);
            }
        }
    }
}
