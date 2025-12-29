using System;
using Common.Entities;

namespace CityHistory.Entities
{
    /// <summary>
    /// Клас для представлення історичного об'єкта (Лаб 9)
    /// </summary>
    [Serializable]
    public class HistoricalObject : Entity
    {
        // Властивості
        public string Name { get; set; }
        public City City { get; set; } // Зв'язок з містом
        public int? YearFounded { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        // Конструктор без параметрів
        public HistoricalObject() : this(null, null, null, null) { }

        // Основний конструктор
        public HistoricalObject(string name, City city, int? yearFounded, string type)
        {
            Name = name;
            City = city;
            YearFounded = yearFounded;
            Type = type;
        }

        // Конструктор з обов'язковими параметрами
        public HistoricalObject(string name, City city)
            : this(name, city, null, null) { }

        // Реалізація властивості Key
        public override string Key { get { return Name; } }

        // Перевизначення ToString
        public override string ToString()
        {
            return string.Format(
                "\tІсторичний об'єкт №{0}\n" +
                "\t Назва: {1}\n" +
                "\t Місто: {2}\n" +
                "\t Рік заснування: {3}\n" +
                "\t Тип: {4}\n" +
                "\t Опис: {5}",
                Id,
                Name,
                City?.Key,
                YearFounded,
                Type,
                Description
            );
        }
    }
}