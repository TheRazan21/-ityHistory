// Файл: CityHistory/Data/IO/BinaryFileIoController.cs
// ВИПРАВЛЕННЯ: Змінено тип з IDataSet на IDataContext
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CityHistory.Data.Interfaces;
using Common.Data.IO;

namespace CityHistory.Data.IO
{
    /// <summary>
    /// Контролер для роботи з бінарними файлами (Лаб 12)
    /// </summary>
    public class BinaryFileIoController : BaseFileTypeInformer, IFileIoController<IDataContext>  
    {
        // Конструктор
        public BinaryFileIoController() : base("Двійкові файли", ".bin") { }

        /// <summary>
        /// Збереження даних у бінарний файл (Лаб 12)
        /// </summary>
        /// <param name="dataSet">Контекст даних</param>
        /// <param name="filePath">Шлях до файлу</param>
        public void Save(IDataContext dataSet, string filePath) 
        {
            // Додаємо розширення
            filePath = Path.ChangeExtension(filePath, FileExtension);

            // Створюємо BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Відкриваємо файл і серіалізуємо дані
            using (FileStream fStream = File.OpenWrite(filePath))
            {
                formatter.Serialize(fStream, dataSet);
            }
        }

        /// <summary>
        /// Завантаження даних з бінарного файлу (Лаб 12)
        /// </summary>
        /// <param name="dataSet">Контекст даних</param>
        /// <param name="filePath">Шлях до файлу</param>
        /// <returns>True якщо завантаження успішне</returns>
        public bool Load(IDataContext dataSet, string filePath)  
        {
            // Додаємо розширення
            filePath = Path.ChangeExtension(filePath, FileExtension);

            // Перевіряємо існування файлу
            if (!File.Exists(filePath))
                return false;

            IDataContext newDataSet = null;  

            // Створюємо BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Відкриваємо файл і десеріалізуємо дані
            using (FileStream fStream = File.OpenRead(filePath))
            {
                newDataSet = (IDataContext)formatter.Deserialize(fStream); 
            }

            // Якщо десеріалізація не вдалася
            if (newDataSet == null)
                return false;

            // Копіюємо дані
            newDataSet.CopyTo(dataSet);

            return true;
        }
    }
}