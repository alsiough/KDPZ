using Microsoft.VisualStudio.CodeCoverage;
using lab03.Models;
using lab03.Data;
using System;
using System.IO;
using Xunit;

namespace RestaurantSchedulingApp.Tests.test
{
    public class SchedulingTests
    {
        // ==========================================
        // БІЗНЕС-ЛОГІКА (Тестування модуля Shift)
        // ==========================================

        // Тест-кейс 1 (Позитивний): Призначення правильного працівника (Пункт 1.1)
        [Fact]
        public void AssignEmployee_CorrectRoleAndAvailable_ReturnsTrue()
        {
            // Arrange (Підготовка)
            var chef = new Employee(1, "Гордон", "Кухар");
            var shiftDate = new DateTime(2026, 5, 4, 8, 0, 0); // 4 травня, 8:00
            var shift = new Shift(1, shiftDate, shiftDate.AddHours(8), "Кухар");

            // Act (Дія)
            bool result = shift.AssignEmployee(chef);

            // Assert (Перевірка)
            Assert.True(result); // Метод має повернути true
            Assert.Equal(chef, shift.AssignedEmployee); // Працівник має бути призначений
            Assert.True(shift.IsFilled); // Зміна має вважатися заповненою
        }

        // Тест-кейс 2 (Негативний): Працівник має вихідний у цей день (Пункт 1.3)
        [Fact]
        public void AssignEmployee_EmployeeOnDayOff_ReturnsFalse()
        {
            // Arrange
            var chef = new Employee(1, "Гордон", "Кухар");
            var shiftDate = new DateTime(2026, 5, 4, 8, 0, 0); // Зміна 4 травня
            var shift = new Shift(1, shiftDate, shiftDate.AddHours(8), "Кухар");

            // Ізоляція: створюємо умову, за якої працівник недоступний
            chef.RequestTimeOff(shiftDate.Date);

            // Act
            bool result = shift.AssignEmployee(chef);

            // Assert
            Assert.False(result); // Метод має відхилити призначення
            Assert.Null(shift.AssignedEmployee); // Зміна має залишитися без працівника
        }

        // ==========================================
        // ДОПОМІЖНІ ЗАВДАННЯ (Тестування модуля FileHandler)
        // ==========================================

        // Тест-кейс 3 (Позитивний): Успішний запис розкладу у файл (Пункт 2.1)
        [Fact]
        public void ExportSchedule_PopulatedSchedule_CreatesCorrectFile()
        {
            // Arrange
            string testFilePath = "test_export_ok.txt";
            var schedule = new Schedule(10, new DateTime(2026, 5, 4));
            var shift = new Shift(1, new DateTime(2026, 5, 4), new DateTime(2026, 5, 4), "Кухар");
            var chef = new Employee(1, "Гордон", "Кухар");

            shift.AssignEmployee(chef);
            schedule.AddShift(shift);

            // Act
            FileHandler.ExportSchedule(schedule, testFilePath);

            // Assert
            Assert.True(File.Exists(testFilePath)); // Перевіряємо, чи файл створився
            string fileContent = File.ReadAllText(testFilePath);
            Assert.Contains("Гордон", fileContent); // Перевіряємо, чи є ім'я працівника у файлі

            // Прибирання після тесту
            File.Delete(testFilePath);
        }

        // Тест-кейс 4 (Негативний/Крайовий): Експорт порожньої зміни не викликає помилок (Пункт 2.2)
        [Fact]
        public void ExportSchedule_UnfilledShift_WritesPlaceholderWithoutCrash()
        {
            // Arrange
            string testFilePath = "test_export_empty.txt";
            var schedule = new Schedule(11, new DateTime(2026, 5, 4));
            var emptyShift = new Shift(2, new DateTime(2026, 5, 4), new DateTime(2026, 5, 4), "Офіціант");

            schedule.AddShift(emptyShift); // Додаємо зміну без працівника (AssignedEmployee == null)

            // Act
            // Якщо тут є баг із NullReferenceException, тест впаде на цьому кроці
            FileHandler.ExportSchedule(schedule, testFilePath);

            // Assert
            Assert.True(File.Exists(testFilePath));
            string fileContent = File.ReadAllText(testFilePath);
            Assert.Contains("НЕ ПРИЗНАЧЕНО", fileContent); // Має спрацювати заглушка замість крашу

            // Прибирання після тесту
            File.Delete(testFilePath);
        }
    }
}