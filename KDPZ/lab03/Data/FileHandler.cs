using System;
using System.IO;
using System.Text;
using lab03.Models;

namespace lab03.Data
{
    // Статичний клас для роботи з файлами
    public static class FileHandler
    {
        // Метод для експорту розкладу у файл. 
        // Примітка: Явно використовується кодування UTF-8 (без підтримки UTF-16).
        public static void ExportSchedule(Schedule schedule, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine($"ID Розкладу: {schedule.Id}");
                    writer.WriteLine($"Тиждень від: {schedule.WeekStartDate.ToShortDateString()}");
                    writer.WriteLine($"Статус: {schedule.Status}");
                    writer.WriteLine("--- Зміни ---");

                    foreach (var shift in schedule.Shifts)
                    {
                        string empName = shift.IsFilled ? shift.AssignedEmployee.Name : "НЕ ПРИЗНАЧЕНО";
                        writer.WriteLine($"{shift.StartTime} - {shift.EndTime} | Роль: {shift.RequiredRole} | Працівник: {empName}");
                    }
                }
                Console.WriteLine("Розклад успішно експортовано у файл.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка запису у файл: {ex.Message}");
            }
        }
    }
}