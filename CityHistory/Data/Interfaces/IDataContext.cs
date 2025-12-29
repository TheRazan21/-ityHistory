// Файл: CityHistory/Data/Interfaces/IDataContext.cs
// ВИПРАВЛЕННЯ: Додано наслідування від IBaseDataSet
using System.Collections.Generic;
using CityHistory.Entities;
using Common.Data.Interfaces;

namespace CityHistory.Data.Interfaces
{
    /// <summary>
    /// Інтерфейс контексту даних ПО "Історія та культура міста" (Лаб 13)
    /// </summary>
    public interface IDataContext : IBaseDataSet  
    {
        // Колекції для кожної сутності
        ICollection<City> Cities { get; }
        ICollection<HistoricalObject> HistoricalObjects { get; }
        ICollection<TouristPlace> TouristPlaces { get; }
        ICollection<PublicSpace> PublicSpaces { get; }
        ICollection<Event> Events { get; }

        // Метод копіювання даних
        void CopyTo(IDataContext dataContext);
    }
}