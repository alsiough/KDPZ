using System;
using System.Collections.Generic;

namespace lab03.Models
{
    // Клас розкладу на тиждень
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime WeekStartDate { get; set; }
        public string Status { get; set; }

        // Композиція: Розклад містить багато змін
        public List<Shift> Shifts { get; private set; }

        public Schedule(int id, DateTime weekStartDate)
        {
            Id = id;
            WeekStartDate = weekStartDate;
            Status = "Чернетка";
            Shifts = new List<Shift>();
        }

        // Додавання зміни до розкладу
        public void AddShift(Shift shift)
        {
            Shifts.Add(shift);
        }

        // Перевіряє, чи всі зміни в розкладі успішно укомплектовані працівниками
        public bool ValidateSchedule()
        {
            foreach (var shift in Shifts)
            {
                if (!shift.IsFilled) return false; // Якщо знайдено порожню зміну
            }
            return true;
        }
    }
}