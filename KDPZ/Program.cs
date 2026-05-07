using System;
using RestaurantSchedulingApp.Models;
using RestaurantSchedulingApp.Data;

namespace RestaurantSchedulingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Ініціалізація працівників
            Employee chef = new Employee(1, "Гордон Рамзі", "Кухар");
            Employee waiter1 = new Employee(2, "Іван Доу", "Офіціант");
            Manager manager = new Manager(99, "Аліса Сміт", 5);

            // Офіціант бере вихідний на 4 травня 2026 року
            waiter1.RequestTimeOff(new DateTime(2026, 05, 04));

            // 2. Ініціалізація нового розкладу
            Schedule weeklySchedule = new Schedule(101, new DateTime(2026, 05, 04));

            // 3. Створення змін
            Shift morningCooking = new Shift(1, new DateTime(2026, 05, 04, 8, 0, 0), new DateTime(2026, 05, 04, 16, 0, 0), "Кухар");
            Shift morningServing = new Shift(2, new DateTime(2026, 05, 04, 9, 0, 0), new DateTime(2026, 05, 04, 17, 0, 0), "Офіціант");

            weeklySchedule.AddShift(morningCooking);
            weeklySchedule.AddShift(morningServing);

            // 4. Призначення працівників на зміни
            morningCooking.AssignEmployee(chef); // Успішно

            // Ця дія завершиться невдало, оскільки Іван взяв вихідний на цю дату
            bool isAssigned = morningServing.AssignEmployee(waiter1);
            if (!isAssigned)
            {
                Console.WriteLine($"Не вдалося призначити {waiter1.Name} на зміну {morningServing.Id}. Працівник недоступний у цей день.");
            }

            // 5. Експорт даних у текстовий файл
            FileHandler.ExportSchedule(weeklySchedule, "Schedule_MayWeek1.txt");
        }
    }
}