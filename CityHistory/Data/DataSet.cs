// Файл: CityHistory/Data/SimpleDataSet.cs
// ВИПРАВЛЕННЯ: Перейменовано клас DataSet в SimpleDataSet та змінено інтерфейс
using System;
using System.Collections.Generic;
using CityHistory.Data.Interfaces;
using CityHistory.Entities;

namespace CityHistory.Data
{
    /// <summary>
    /// Простий набір даних (Лаб 10)
    /// </summary>
    [Serializable]
    public class SimpleDataSet : IDataContext  // ✅ ЗМІНЕНО: був DataSet, тепер SimpleDataSet і реалізує IDataContext
    {
        // Приватні поля для колекцій
        protected readonly ICollection<City> _cities;
        protected readonly ICollection<HistoricalObject> _historicalObjects;
        protected readonly ICollection<TouristPlace> _touristPlaces;
        protected readonly ICollection<PublicSpace> _publicSpaces;
        protected readonly ICollection<Event> _events;

        // Конструктор без параметрів
        public SimpleDataSet() : this(
            new List<City>(),
            new List<HistoricalObject>(),
            new List<TouristPlace>(),
            new List<PublicSpace>(),
            new List<Event>())
        { }

        // Основний конструктор
        public SimpleDataSet(
            ICollection<City> cities,
            ICollection<HistoricalObject> historicalObjects,
            ICollection<TouristPlace> touristPlaces,
            ICollection<PublicSpace> publicSpaces,
            ICollection<Event> events)
        {
            _cities = cities;
            _historicalObjects = historicalObjects;
            _touristPlaces = touristPlaces;
            _publicSpaces = publicSpaces;
            _events = events;
        }

        // Реалізація властивостей інтерфейсу IDataContext
        public ICollection<City> Cities => _cities;
        public ICollection<HistoricalObject> HistoricalObjects => _historicalObjects;
        public ICollection<TouristPlace> TouristPlaces => _touristPlaces;
        public ICollection<PublicSpace> PublicSpaces => _publicSpaces;
        public ICollection<Event> Events => _events;

        // Реалізація методів інтерфейсу IBaseDataSet
        public virtual void Clear()
        {
            Cities.Clear();
            HistoricalObjects.Clear();
            TouristPlaces.Clear();
            PublicSpaces.Clear();
            Events.Clear();
        }

        public virtual bool IsEmpty()
        {
            return Cities.Count == 0 &&
                   HistoricalObjects.Count == 0 &&
                   TouristPlaces.Count == 0 &&
                   PublicSpaces.Count == 0 &&
                   Events.Count == 0;
        }

        // Реалізація методу інтерфейсу IDataContext
        public virtual void CopyTo(IDataContext other)
        {
            foreach (var obj in Cities) other.Cities.Add(obj);
            foreach (var obj in HistoricalObjects) other.HistoricalObjects.Add(obj);
            foreach (var obj in TouristPlaces) other.TouristPlaces.Add(obj);
            foreach (var obj in PublicSpaces) other.PublicSpaces.Add(obj);
            foreach (var obj in Events) other.Events.Add(obj);
        }
    }
}