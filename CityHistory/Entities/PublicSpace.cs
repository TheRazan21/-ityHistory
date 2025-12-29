using System;
using Common.Entities;

namespace CityHistory.Entities
{
    /// <summary>
    /// Клас для представлення громадського простору (Лаб 9)
    /// </summary>
    [Serializable]
    public class PublicSpace : Entity
    {
        // Властивості
        public string Name { get; set; }
        public City City { get; set; }
        public string Type { get; set; }
        public double? Area { get; set; }
        public string Description { get; set; }

        // Конструктор без параметрів
        public PublicSpace() : this(null, null, null, null) { }

        // Основний конструктор
        public PublicSpace(string name, City city, string type, double? area)
        {
            Name = name;
            City = city;
            Type = type;
            Area = area;
        }

        // Конструктор з обов'язковими параметрами
        public PublicSpace(string name, City city)
            : this(name, city, null, null) { }

        // Реалізація властивості Key
        public override string Key { get { return Name; } }

        // Перевизначення ToString
        public override string ToString()
        {
            return string.Format(
                "\tГромадський простір №{0}\n" +
                "\t Назва: {1}\n" +
                "\t Місто: {2}\n" +
                "\t Тип: {3}\n" +
                "\t Площа: {4} км²\n" +
                "\t Опис: {5}",
                Id,
                Name,
                City?.Key,
                Type,
                Area,
                Description
            );
        }
    }
}