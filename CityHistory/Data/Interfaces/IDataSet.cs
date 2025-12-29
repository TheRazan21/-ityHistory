using System.Collections.Generic;
using CityHistory.Entities;
using Common.Data.Interfaces;

namespace CityHistory.Data.Interfaces
{
    /// <summary>
    /// Інтерфейс набору даних ПО (Лаб 10)
    /// </summary>
    public interface IDataSet : IBaseDataSet
    {
        // Колекції для кожної сутності
        ICollection<City> Cities { get; }
        ICollection<HistoricalObject> HistoricalObjects { get; }
        ICollection<TouristPlace> TouristPlaces { get; }
        ICollection<PublicSpace> PublicSpaces { get; }
        ICollection<Event> Events { get; }

        // Метод копіювання даних
        void CopyTo(IDataSet dataSet);
    }
}