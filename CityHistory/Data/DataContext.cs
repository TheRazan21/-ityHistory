// Файл: CityHistory/Data/DataContext.cs
// ВИПРАВЛЕННЯ: Виправлено базовий клас і типи
using System;
using System.Collections.Generic;
using CityHistory.Data.Interfaces;
using CityHistory.Data.Testing;
using CityHistory.Entities;
using Common.Data;
using Common.Data.IO;

namespace CityHistory.Data
{
    /// <summary>
    /// Реалізація контексту даних для системи "Історія та культура міста" (Лаб 13)
    /// </summary>
    [Serializable]
    public class DataContext : BaseDataContext<IDataContext>, IDataContext  // ✅ ВИПРАВЛЕНО: тепер використовує IDataContext
    {
        /// <summary>
        /// Конструктор з контролером введення/виведення
        /// </summary>
        /// <param name="fileIoController">Контролер введення/виведення файлів</param>
        public DataContext(IFileIoController<IDataContext> fileIoController)  // ✅ ВИПРАВЛЕНО: тип параметру
            : this(fileIoController, new SimpleDataSet(), "", "CityHistory")
        {
        }

        /// <summary>
        /// Параметризований конструктор
        /// </summary>
        /// <param name="fileIoController">Контролер введення/виведення файлів</param>
        /// <param name="dataSet">Набір даних</param>
        /// <param name="directoryName">Ім'я каталогу</param>
        /// <param name="fileName">Ім'я файлу</param>
        public DataContext(IFileIoController<IDataContext> fileIoController,  // ✅ ВИПРАВЛЕНО: тип параметру
                          IDataContext dataSet, string directoryName, string fileName)
            : base(fileIoController, dataSet, directoryName, fileName)
        {
        }

        /// <summary>
        /// Колекція міст
        /// </summary>
        public ICollection<City> Cities => DataSet.Cities;

        /// <summary>
        /// Колекція історичних об'єктів
        /// </summary>
        public ICollection<HistoricalObject> HistoricalObjects => DataSet.HistoricalObjects;

        /// <summary>
        /// Колекція туристичних місць
        /// </summary>
        public ICollection<TouristPlace> TouristPlaces => DataSet.TouristPlaces;

        /// <summary>
        /// Колекція громадських просторів
        /// </summary>
        public ICollection<PublicSpace> PublicSpaces => DataSet.PublicSpaces;

        /// <summary>
        /// Колекція подій
        /// </summary>
        public ICollection<Event> Events => DataSet.Events;

        /// <summary>
        /// Копіювання даних в інший контекст
        /// </summary>
        /// <param name="dataContext">Цільовий контекст даних</param>
        public void CopyTo(IDataContext dataContext)
        {
            DataSet.CopyTo(dataContext);
        }

        /// <summary>
        /// Створення тестових даних
        /// </summary>
        /// <returns>True якщо створення успішне</returns>
        public bool CreateTestingData()
        {
            bool result = DataSet.CreateTestingData();  // ✅ ВИПРАВЛЕНО: тепер метод існує в IDataContext
            if (result) OnDataChanged();
            return result;
        }

        /// <summary>
        /// Текстове представлення контексту даних
        /// </summary>
        /// <returns>Рядок з інформацією про дані</returns>
        public override string ToString()
        {
            // ✅ ВИПРАВЛЕНО: використовуємо стандартний ToString, оскільки ToDataString тепер в розширеннях
            return $"DataContext: Cities={Cities.Count}, HistoricalObjects={HistoricalObjects.Count}, " +
                   $"TouristPlaces={TouristPlaces.Count}, PublicSpaces={PublicSpaces.Count}, Events={Events.Count}";
        }
    }
}