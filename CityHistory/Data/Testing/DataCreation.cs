using System;
using System.Collections.Generic;
using System.Linq;
using CityHistory.Data.Interfaces;
using CityHistory.Entities;

namespace CityHistory.Data.Testing
{
    public static class DataCreation
    {
        public static void CreateTestingCities(this ICollection<City> cities)
        {
            // ВИПРАВЛЕНО: Використання Object Initializer
            cities.Add(new City
            {
                Id = 1,
                Name = "Львів",
                Country = "Україна",
                Area = 182.01,
                Population = 717000,
                Description = "Місто з багатою історією, центр Галичини."
            });

            cities.Add(new City
            {
                Id = 2,
                Name = "Київ",
                Country = "Україна",
                Area = 847.66,
                Population = 2804000,
                Description = "Столиця України."
            });

            cities.Add(new City
            {
                Id = 3,
                Name = "Одеса",
                Country = "Україна",
                Area = 162.42,
                Population = 1010000,
                Description = "Перлина біля моря."
            });
        }

        private static void CreateTestingHistoricalObjects(IDataContext dataSet)
        {
            var cities = dataSet.Cities.ToList();

            dataSet.HistoricalObjects.Add(new HistoricalObject(
                "Львівська ратуша",
                cities.FirstOrDefault(c => c.Name == "Львів"),
                1835,
                "Ратуша")
            {
                Id = 1,
                Description = "Символ міста."
            });
            // ... можна додати інші об'єкти аналогічно
        }

        // Інші методи CreateTesting... (для TouristPlaces тощо) залишаються схожими,
        // головне - скрізь використовувати IDataContext замість IDataSet

        private static void CreateTestingTouristPlaces(IDataContext dataSet)
        {
            var cities = dataSet.Cities.ToList();
            dataSet.TouristPlaces.Add(new TouristPlace("Високий замок", cities.FirstOrDefault(c => c.Name == "Львів"), "Природне", 0m) { Id = 1 });
        }

        private static void CreateTestingPublicSpaces(IDataContext dataSet)
        {
            var cities = dataSet.Cities.ToList();
            dataSet.PublicSpaces.Add(new PublicSpace("Площа Ринок", cities.FirstOrDefault(c => c.Name == "Львів"), "Площа", 0.014) { Id = 1 });
        }

        private static void CreateTestingEvents(IDataContext dataSet)
        {
            var cities = dataSet.Cities.ToList();
            dataSet.Events.Add(new Event("Львівський книжковий форум", cities.FirstOrDefault(c => c.Name == "Львів"), new DateTime(2024, 9, 15), "Фестиваль") { Id = 1 });
        }

        public static bool CreateTestingData(this IDataContext dataSet)
        {
            if (dataSet.IsEmpty())
            {
                CreateTestingCities(dataSet.Cities);
                CreateTestingHistoricalObjects(dataSet);
                CreateTestingTouristPlaces(dataSet);
                CreateTestingPublicSpaces(dataSet);
                CreateTestingEvents(dataSet);
                return true;
            }
            return false;
        }
    }
}