using System;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CountryInfo
{
    internal class DBConnecter
    {
        private string ConnectionString { get; set; }
        private DataContext db;
        private Table<Cities> tbCities;
        private Table<Countries> tbCountries;
        private Table<Regions> tbRegions;

        /// <summary>
        /// Конструктор класса для работы с БД
        /// </summary>
        /// <param name="connection"></param>
        /// Принимает на вход строку подключения к БД
        public DBConnecter()
        {
            ConnectionString = GetConnectionString();
            db = new DataContext(ConnectionString);
            tbCities = db.GetTable<Cities>();
            tbCountries = db.GetTable<Countries>();
            tbRegions = db.GetTable<Regions>();
        }

        /// <summary>
        /// Получение строки подключения к БД из файла
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string path = @"..\..\ConnectionString.txt";

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Добавление города
        /// </summary>
        /// <param name="city"></param>
        /// Название города
        public void AddCity(Cities city)
        {
            db.GetTable<Cities>().InsertOnSubmit(city);
            UpdateDB();
        }


        /// <summary>
        /// Получение города по названию
        /// </summary>
        /// <param name="cityTitle"></param>
        /// Название города
        /// <returns></returns>
        /// Возвращает объект-город
        public Cities GetCityByTitle(string cityTitle)
        {
            var query = from city in tbCities
                        where city.Title == cityTitle
                        select city;
            if (query.Count() == 0)
            {
                return null;
            }
            else
            {
                return query.First();
            }
        }


        /// <summary>
        /// Получение города по ID
        /// </summary>
        /// <param name="cityTitle"></param>
        /// ID города
        /// <returns></returns>
        /// Возвращает объект-город
        public Cities GetCityById(int cityId)
        {
            var query = from city in tbCities
                        where city.Id == cityId
                        select city;
            if (query.Count() == 0)
            {
                return null;
            }
            else
            {
                return query.First();
            }
        }

        /// <summary>
        /// Добавление регоиона
        /// </summary>
        /// <param name="region"></param>
        /// Принимает объект-регион
        public void AddRegion(Regions region)
        {
            db.GetTable<Regions>().InsertOnSubmit(region);
            UpdateDB();
        }

        /// <summary>
        /// Получение региона по названию
        /// </summary>
        /// <param name="regionTitle"></param>
        /// Принимает название региона
        /// <returns></returns>
        /// Возвращает объект-регион
        public Regions GetRegionByTitle(String regionTitle)
        {
            var query = from region in tbRegions
                        where region.Title == regionTitle
                        select region;
            if (query.Count() == 0)
            {
                return null;
            }
            else
            {
                return query.First();
            }
        }


        /// <summary>
        /// Получение региона по ID
        /// </summary>
        /// <param name="regionTitle"></param>
        /// Принимает ID региона
        /// <returns></returns>
        /// Возвращает объект-регион
        public Regions GetRegionById(int regionId)
        {
            var query = from region in tbRegions
                        where region.Id == regionId
                        select region;
            if (query.Count() == 0)
            {
                return null;
            }
            else
            {
                return query.First();
            }
        }

        /// <summary>
        /// Добавление страны
        /// </summary>
        /// <param name="country"></param>
        /// Принимает объект-страну
        public void AddCountry(Countries country)
        {
            db.GetTable<Countries>().InsertOnSubmit(country);
            UpdateDB();
        }


        /// <summary>
        /// Получение страны по коду
        /// </summary>
        /// <param name="countryCode"></param>
        /// Принимает код страны
        /// <returns></returns>
        /// Возвращает объект-страну
        public Countries GetCountryByCode(string countryCode)
        {
            var query = from country in tbCountries
                        where country.Code == countryCode
                        select country;
            if (query.Count() == 0)
            {
                return null;
            }
            else
            {
                return query.First();
            }
        }


        /// <summary>
        /// Получение всех стран
        /// </summary>
        /// <returns></returns>
        /// Возвращает список объектов-стран
        public IQueryable<Countries> GetAllCountries()
        {
            var query = from country in tbCountries
                        select country;
            return query;
        }


        /// <summary>
        /// Обновление изменений БД
        /// </summary>
        public void UpdateDB()
        {
            db.SubmitChanges();
        }

    }
}
