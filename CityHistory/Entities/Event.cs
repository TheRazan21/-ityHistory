using System;
using Common.Entities;

namespace CityHistory.Entities
{
    /// <summary>
    /// Клас для представлення події (Лаб 9)
    /// </summary>
    [Serializable]
    public class Event : Entity
    {
        // Властивості
        public string Name { get; set; }
        public City City { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        // Конструктор без параметрів
        public Event() : this(null, null, null, null) { }

        // Основний конструктор
        public Event(string name, City city, DateTime? date, string type)
        {
            Name = name;
            City = city;
            Date = date;
            Type = type;
        }

        // Конструктор з обов'язковими параметрами
        public Event(string name, City city)
            : this(name, city, null, null) { }

        // Реалізація властивості Key
        public override string Key
        {
            get
            {
                return string.Format("{0} - {1}", Name,
                    Date?.ToString("dd.MM.yyyy") ?? "дата невідома");
            }
        }

        // Перевизначення ToString
        public override string ToString()
        {
            return string.Format(
                "\tПодія №{0}\n" +
                "\t Назва: {1}\n" +
                "\t Місто: {2}\n" +
                "\t Дата: {3}\n" +
                "\t Тип: {4}\n" +
                "\t Опис: {5}",
                Id,
                Name,
                City?.Key,
                Date?.ToString("dd.MM.yyyy"),
                Type,
                Description
            );
        }
    }
}