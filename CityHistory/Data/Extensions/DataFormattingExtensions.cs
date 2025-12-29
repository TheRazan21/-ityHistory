// Файл: CityHistory/Extensions/DataFormattingExtensions.cs
// ВИПРАВЛЕННЯ: Виправлено назви властивостей
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CityHistory.Data.Interfaces;
using CityHistory.Entities;

namespace CityHistory.Extensions
{
    /// <summary>
    /// Методи розширення для форматування даних системи "Історія та культура міста"
    /// </summary>
    public static class DataFormattingExtensions
    {
        /// <summary>
        /// Перетворення контексту даних у текстове представлення
        /// </summary>
        /// <param name="dataContext">Контекст даних</param>
        /// <returns>Текстове представлення всіх даних</returns>
        public static string ToDataString(this IDataContext dataContext)
        {
            if (dataContext == null)
                return "null";

            var sb = new StringBuilder();

            sb.AppendLine("=== СИСТЕМА \"ІСТОРІЯ ТА КУЛЬТУРА МІСТА\" ===");
            sb.AppendLine();

            // Статистична інформація
            sb.AppendLine($"Загальна кількість міст: {dataContext.Cities.Count}");
            sb.AppendLine($"Загальна кількість історичних об'єктів: {dataContext.HistoricalObjects.Count}");
            sb.AppendLine($"Загальна кількість туристичних місць: {dataContext.TouristPlaces.Count}");
            sb.AppendLine($"Загальна кількість громадських просторів: {dataContext.PublicSpaces.Count}");
            sb.AppendLine($"Загальна кількість подій: {dataContext.Events.Count}");
            sb.AppendLine();

            // Міста
            if (dataContext.Cities.Any())
            {
                sb.AppendLine("МІСТА:");
                sb.AppendLine(new string('-', 60));
                foreach (var city in dataContext.Cities.OrderBy(c => c.Name))
                {
                    sb.AppendLine(city.ToString());
                    sb.AppendLine();
                }
            }

            // Історичні об'єкти
            if (dataContext.HistoricalObjects.Any())
            {
                sb.AppendLine("ІСТОРИЧНІ ОБ'ЄКТИ:");
                sb.AppendLine(new string('-', 60));
                foreach (var obj in dataContext.HistoricalObjects.OrderBy(o => o.City?.Name).ThenBy(o => o.Name))
                {
                    sb.AppendLine($"Назва: {obj.Name}");
                    sb.AppendLine($"Місто: {obj.City?.Name ?? "не вказано"}");
                    sb.AppendLine($"Рік заснування: {obj.YearFounded?.ToString() ?? "невідомо"}"); 
                    sb.AppendLine($"Тип: {obj.Type ?? "не вказано"}");
                    sb.AppendLine($"Опис: {obj.Description ?? "відсутній"}");
                    sb.AppendLine();
                }
            }

            // Туристичні місця
            if (dataContext.TouristPlaces.Any())
            {
                sb.AppendLine("ТУРИСТИЧНІ МІСЦЯ:");
                sb.AppendLine(new string('-', 60));
                foreach (var place in dataContext.TouristPlaces.OrderBy(p => p.City?.Name).ThenBy(p => p.Name))
                {
                    sb.AppendLine($"Назва: {place.Name}");
                    sb.AppendLine($"Місто: {place.City?.Name ?? "не вказано"}");
                    sb.AppendLine($"Категорія: {place.Category ?? "не вказано"}");
                    sb.AppendLine($"Вартість відвідування: {place.EntranceFee?.ToString() ?? "безкоштовно"} грн"); 
                    sb.AppendLine($"Опис: {place.Description ?? "відсутній"}");
                    sb.AppendLine();
                }
            }

            // Громадські простори
            if (dataContext.PublicSpaces.Any())
            {
                sb.AppendLine("ГРОМАДСЬКІ ПРОСТОРИ:");
                sb.AppendLine(new string('-', 60));
                foreach (var space in dataContext.PublicSpaces.OrderBy(s => s.City?.Name).ThenBy(s => s.Name))
                {
                    sb.AppendLine($"Назва: {space.Name}");
                    sb.AppendLine($"Місто: {space.City?.Name ?? "не вказано"}");
                    sb.AppendLine($"Тип: {space.Type ?? "не вказано"}");
                    sb.AppendLine($"Площа: {space.Area?.ToString() ?? "невідома"} км²");
                    sb.AppendLine($"Опис: {space.Description ?? "відсутній"}");
                    sb.AppendLine();
                }
            }

            // Події
            if (dataContext.Events.Any())
            {
                sb.AppendLine("ПОДІЇ:");
                sb.AppendLine(new string('-', 60));
                foreach (var eventItem in dataContext.Events.OrderBy(e => e.Date).ThenBy(e => e.Name))
                {
                    sb.AppendLine($"Назва: {eventItem.Name}");
                    sb.AppendLine($"Місто: {eventItem.City?.Name ?? "не вказано"}");
                    sb.AppendLine($"Дата: {eventItem.Date?.ToString("dd.MM.yyyy") ?? "невідома"}");
                    sb.AppendLine($"Тип: {eventItem.Type ?? "не вказано"}");
                    sb.AppendLine($"Опис: {eventItem.Description ?? "відсутній"}");
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Статистичне представлення контексту даних
        /// </summary>
        /// <param name="dataContext">Контекст даних</param>
        /// <param name="header">Заголовок</param>
        /// <returns>Статистична інформація</returns>
        public static string ToStatisticsString(this IDataContext dataContext, string header = null)
        {
            if (header == null)
                header = "Статистичні дані про об'єкти ПО";

            if (dataContext == null || dataContext.IsEmpty())
            {
                return string.Format("{0}\n (дані відсутні)", header);
            }

            return string.Format("{0}\n" +
                " Представлено:\n" +
                "{1,7} міст\n" +
                "{2,7} історичних об'єктів\n" +
                "{3,7} туристичних місць\n" +
                "{4,7} громадських просторів\n" +
                "{5,7} подій",
                header,
                dataContext.Cities.Count,
                dataContext.HistoricalObjects.Count,
                dataContext.TouristPlaces.Count,
                dataContext.PublicSpaces.Count,
                dataContext.Events.Count
            );
        }

        /// <summary>
        /// Перевірка на порожність контексту даних
        /// </summary>
        /// <param name="dataContext">Контекст даних</param>
        /// <returns>True якщо всі колекції порожні</returns>
        public static bool IsEmpty(this IDataContext dataContext)
        {
            if (dataContext == null) return true;

            return dataContext.Cities.Count == 0 &&
                   dataContext.HistoricalObjects.Count == 0 &&
                   dataContext.TouristPlaces.Count == 0 &&
                   dataContext.PublicSpaces.Count == 0 &&
                   dataContext.Events.Count == 0;
        }
    }
}