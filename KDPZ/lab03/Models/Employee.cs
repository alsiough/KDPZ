using System;
using System.Collections.Generic;

namespace lab03.Models
{
    // Базовий клас для всіх працівників ресторану
    public class Employee
    {
        // Автоматично реалізовані властивості (Інкапсуляція)
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // напр., "Офіціант", "Кухар", "Бармен"

        // Приватний список для відстеження днів, коли працівник попросив вихідний
        private List<DateTime> unavailableDates;

        // Конструктор для ініціалізації працівника
        public Employee(int id, string name, string role)
        {
            Id = id;
            Name = name;
            Role = role;
            unavailableDates = new List<DateTime>();
        }

        // Метод для додавання запиту на вихідний день
        public void RequestTimeOff(DateTime date)
        {
            if (!unavailableDates.Contains(date))
            {
                unavailableDates.Add(date);
            }
        }

        // Перевіряє, чи доступний працівник у вказану дату
        public bool IsAvailable(DateTime date)
        {
            return !unavailableDates.Contains(date.Date);
        }

        // Віртуальний метод, який можна перевизначити у похідних класах (Поліморфізм)
        public virtual string GetDetails()
        {
            return $"[ID: {Id}] {Name} - {Role}";
        }
    }
}