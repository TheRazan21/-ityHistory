using System;
using Common.Entities;

namespace CityHistory.Entities
{
    /// <summary>
    /// Клас для представлення туристичного місця (Лаб 9)
    /// </summary>
    [Serializable]
    public class TouristPlace : Entity
    {
        // Властивості
        public string Name { get; set; }
        public City City { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal? EntranceFee { get; set; }

        // Конструктор без параметрів
        public TouristPlace() : this(null, null, null, null) { }

        // Основний конструктор
        public TouristPlace(string name, City city, string category, decimal? entranceFee)
        {
            Name = name;
            City = city;
            Category = category;
            EntranceFee = entranceFee;
        }

        // Конструктор з обов'язковими параметрами
        public TouristPlace(string name, City city)
            : this(name, city, null, null) { }

        // Реалізація властивості Key
        public override string Key { get { return Name; } }

        // Перевизначення ToString
        public override string ToString()
        {
            return string.Format(
                "\tТуристичне місце №{0}\n" +
                "\t Назва: {1}\n" +
                "\t Місто: {2}\n" +
                "\t Категорія: {3}\n" +
                "\t Вартість відвідування: {4} грн\n" +
                "\t Опис: {5}",
                Id,
                Name,
                City?.Key,
                Category,
                EntranceFee,
                Description
            );
        }
    }
}