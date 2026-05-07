using System;

namespace RestaurantSchedulingApp.Models
{
    // Клас робочої зміни
    public class Shift
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string RequiredRole { get; set; } // Необхідна посада для зміни

        // Агрегація: Зміна МАЄ (HAS-A) призначеного працівника
        public Employee AssignedEmployee { get; private set; }

        // Властивість, що повертає true, якщо зміну закрито працівником
        public bool IsFilled => AssignedEmployee != null;

        public Shift(int id, DateTime startTime, DateTime endTime, string requiredRole)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            RequiredRole = requiredRole;
        }

        // Спроба призначити працівника на зміну (перевіряє роль та доступність)
        public bool AssignEmployee(Employee emp)
        {
            if (emp.Role == RequiredRole && emp.IsAvailable(StartTime))
            {
                AssignedEmployee = emp;
                return true;
            }
            return false;
        }

        // Метод для розрахунку тривалості зміни в годинах
        public double GetDurationInHours()
        {
            return (EndTime - StartTime).TotalHours;
        }
    }
}