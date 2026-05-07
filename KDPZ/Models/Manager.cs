using System;

namespace RestaurantSchedulingApp.Models
{
    // Клас Manager успадковує Employee (Наслідування - відношення "is-a")
    public class Manager : Employee
    {
        public int AccessLevel { get; set; } // Рівень доступу менеджера

        // Викликає конструктор базового класу за допомогою ключового слова 'base'
        public Manager(int id, string name, int accessLevel)
            : base(id, name, "Менеджер")
        {
            AccessLevel = accessLevel;
        }

        // Перевизначення базового методу для надання специфічної інформації
        public override string GetDetails()
        {
            return $"[ID МЕНЕДЖЕРА: {Id}] {Name} (Рівень доступу: {AccessLevel})";
        }
    }
}