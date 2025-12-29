using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq; // Додано для XElement
using CityHistory.Data.Interfaces;
using CityHistory.Entities;
using Common.Data.IO;

namespace CityHistory.Data.IO
{
    public class XmlFileIoController : BaseFileTypeInformer, IFileIoController<IDataContext>
    {
        public XmlFileIoController() : base("Файли формату XML", ".xml") { }

        public void Save(IDataContext dataSet, string filePath)
        {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("CityHistory");
                WriteData(dataSet, writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        protected virtual void WriteData(IDataContext dataSet, XmlWriter writer)
        {
            WriteCities(dataSet.Cities, writer);
            WriteHistoricalObjects(dataSet.HistoricalObjects, writer);
            WriteTouristPlaces(dataSet.TouristPlaces, writer);
            WritePublicSpaces(dataSet.PublicSpaces, writer);
            WriteEvents(dataSet.Events, writer);
        }

        public bool Load(IDataContext dataSet, string filePath)
        {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            if (!File.Exists(filePath)) return false;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create(filePath, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        ReadData(dataSet, reader);
                    }
                }
            }
            return true;
        }

        protected virtual void ReadData(IDataContext dataSet, XmlReader reader)
        {
            switch (reader.Name)
            {
                case "City": ReadCity(reader, dataSet.Cities); break;
                case "HistoricalObject": ReadHistoricalObject(reader, dataSet); break;
                case "TouristPlace": ReadTouristPlace(reader, dataSet); break;
                case "PublicSpace": ReadPublicSpace(reader, dataSet); break;
                case "Event": ReadEvent(reader, dataSet); break;
            }
        }

        private void WriteCities(IEnumerable<City> collection, XmlWriter writer)
        {
            writer.WriteStartElement("CitiesData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("City");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("Country", obj.Country);

                // ВИПРАВЛЕНО: Прибрано '?', бо тип double/int не nullable
                writer.WriteElementString("Area", obj.Area.ToString());
                writer.WriteElementString("Population", obj.Population.ToString());

                writer.WriteElementString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        // --- Метод для читання ---
        private void ReadCity(XmlReader reader, ICollection<City> collection)
        {
            // Використовуємо XNode для зручного читання атрибутів
            XNode node = XNode.ReadFrom(reader);
            XElement element = node as XElement;

            if (element != null)
            {
                var obj = new City();
                // Безпечне зчитування: пробуємо (int?), якщо null -> 0
                obj.Id = (int?)element.Element("Id") ?? (int?)element.Attribute("Id") ?? 0;
                obj.Name = (string)element.Element("Name") ?? (string)element.Attribute("Name") ?? "";
                obj.Country = (string)element.Element("Country") ?? (string)element.Attribute("Country") ?? "";

                // Зчитуємо Area та Population з перевіркою на null
                obj.Area = (double?)element.Element("Area") ?? (double?)element.Attribute("Area") ?? 0.0;
                obj.Population = (int?)element.Element("Population") ?? (int?)element.Attribute("Population") ?? 0;

                obj.Description = (string)element.Element("Description") ?? (string)element.Attribute("Description") ?? "";

                collection.Add(obj);
            }
        }

        // --- Інші методи запису (залишаємо як було, бо там поля можуть бути nullable) ---

        private void WriteHistoricalObjects(IEnumerable<HistoricalObject> collection, XmlWriter writer)
        {
            writer.WriteStartElement("HistoricalObjectsData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("HistoricalObject");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("CityId", (obj.City?.Id ?? 0).ToString());
                writer.WriteElementString("YearFounded", obj.YearFounded?.ToString() ?? "");
                writer.WriteElementString("Type", obj.Type);
                writer.WriteElementString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteTouristPlaces(IEnumerable<TouristPlace> collection, XmlWriter writer)
        {
            writer.WriteStartElement("TouristPlacesData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("TouristPlace");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("CityId", (obj.City?.Id ?? 0).ToString());
                writer.WriteElementString("Category", obj.Category);
                writer.WriteElementString("EntranceFee", obj.EntranceFee?.ToString() ?? "");
                writer.WriteElementString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WritePublicSpaces(IEnumerable<PublicSpace> collection, XmlWriter writer)
        {
            writer.WriteStartElement("PublicSpacesData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("PublicSpace");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("CityId", (obj.City?.Id ?? 0).ToString());
                writer.WriteElementString("Type", obj.Type);
                writer.WriteElementString("Area", obj.Area?.ToString() ?? "");
                writer.WriteElementString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteEvents(IEnumerable<Event> collection, XmlWriter writer)
        {
            writer.WriteStartElement("EventsData");
            foreach (var obj in collection)
            {
                writer.WriteStartElement("Event");
                writer.WriteElementString("Id", obj.Id.ToString());
                writer.WriteElementString("Name", obj.Name);
                writer.WriteElementString("CityId", (obj.City?.Id ?? 0).ToString());
                writer.WriteElementString("Date", obj.Date?.ToString("yyyy-MM-dd") ?? "");
                writer.WriteElementString("Type", obj.Type);
                writer.WriteElementString("Description", obj.Description);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        // --- Методи читання інших сутностей ---

        private void ReadHistoricalObject(XmlReader reader, IDataContext dataSet)
        {
            XElement element = XNode.ReadFrom(reader) as XElement;
            if (element != null)
            {
                var obj = new HistoricalObject();
                obj.Id = (int?)element.Element("Id") ?? 0;
                obj.Name = (string)element.Element("Name");
                int cityId = (int?)element.Element("CityId") ?? 0;
                obj.City = dataSet.Cities.FirstOrDefault(c => c.Id == cityId);
                obj.YearFounded = (int?)element.Element("YearFounded");
                obj.Type = (string)element.Element("Type");
                obj.Description = (string)element.Element("Description");
                dataSet.HistoricalObjects.Add(obj);
            }
        }

        private void ReadTouristPlace(XmlReader reader, IDataContext dataSet)
        {
            XElement element = XNode.ReadFrom(reader) as XElement;
            if (element != null)
            {
                var obj = new TouristPlace();
                obj.Id = (int?)element.Element("Id") ?? 0;
                obj.Name = (string)element.Element("Name");
                int cityId = (int?)element.Element("CityId") ?? 0;
                obj.City = dataSet.Cities.FirstOrDefault(c => c.Id == cityId);
                obj.Category = (string)element.Element("Category");
                obj.EntranceFee = (decimal?)element.Element("EntranceFee");
                obj.Description = (string)element.Element("Description");
                dataSet.TouristPlaces.Add(obj);
            }
        }

        private void ReadPublicSpace(XmlReader reader, IDataContext dataSet)
        {
            XElement element = XNode.ReadFrom(reader) as XElement;
            if (element != null)
            {
                var obj = new PublicSpace();
                obj.Id = (int?)element.Element("Id") ?? 0;
                obj.Name = (string)element.Element("Name");
                int cityId = (int?)element.Element("CityId") ?? 0;
                obj.City = dataSet.Cities.FirstOrDefault(c => c.Id == cityId);
                obj.Type = (string)element.Element("Type");
                obj.Area = (double?)element.Element("Area");
                obj.Description = (string)element.Element("Description");
                dataSet.PublicSpaces.Add(obj);
            }
        }

        private void ReadEvent(XmlReader reader, IDataContext dataSet)
        {
            XElement element = XNode.ReadFrom(reader) as XElement;
            if (element != null)
            {
                var obj = new Event();
                obj.Id = (int?)element.Element("Id") ?? 0;
                obj.Name = (string)element.Element("Name");
                int cityId = (int?)element.Element("CityId") ?? 0;
                obj.City = dataSet.Cities.FirstOrDefault(c => c.Id == cityId);
                obj.Date = (DateTime?)element.Element("Date");
                obj.Type = (string)element.Element("Type");
                obj.Description = (string)element.Element("Description");
                dataSet.Events.Add(obj);
            }
        }
    }
}