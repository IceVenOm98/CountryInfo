using System;
using System.Data.Linq;
using System.Linq;

namespace CountryInfo
{
    internal class DBConnecter
    {
        private String connectionString { get; set; }
        private DataContext db;
        private Table<Cities> tbCities;
        private Table<Countries> tbCountries;
        private Table<Regions> tbRegions;
        /// <summary>
        /// Конструктор класса для работы с БД
        /// </summary>
        /// <param name="connection"></param>
        /// Принимает на вход строку подключения к БД
        public DBConnecter(String connection)
        {
            connectionString = connection;
            db = new DataContext(connectionString);
            tbCities = db.GetTable<Cities>();
            tbCountries = db.GetTable<Countries>();
            tbRegions = db.GetTable<Regions>();
        }


        /// <summary>
        /// Добавление города
        /// </summary>
        /// <param name="city"></param>
        /// Название города
        public void addCity(Cities city)
        {
            db.GetTable<Cities>().InsertOnSubmit(city);
            updateDB();
        }


        /// <summary>
        /// Получение города по названию
        /// </summary>
        /// <param name="cityTitle"></param>
        /// Название города
        /// <returns></returns>
        /// Возвращает объект-город
        public Cities getCityByTitle(String cityTitle)
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
        public Cities getCityById(int cityId)
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
        public void addRegion(Regions region)
        {
            db.GetTable<Regions>().InsertOnSubmit(region);
            updateDB();
        }

        /// <summary>
        /// Получение региона по названию
        /// </summary>
        /// <param name="regionTitle"></param>
        /// Принимает название региона
        /// <returns></returns>
        /// Возвращает объект-регион
        public Regions getRegionByTitle(String regionTitle)
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
        public Regions getRegionById(int regionId)
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
        public void addCountry(Countries country)
        {
            db.GetTable<Countries>().InsertOnSubmit(country);
            updateDB();
        }


        /// <summary>
        /// Получение страны по коду
        /// </summary>
        /// <param name="countryCode"></param>
        /// Принимает код страны
        /// <returns></returns>
        /// Возвращает объект-страну
        public Countries getCountryByCode(string countryCode)
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
        public IQueryable<Countries> getAllCountries()
        {
            var query = from country in tbCountries
                        select country;
            return query;
        }


        /// <summary>
        /// Обновление изменений БД
        /// </summary>
        public void updateDB()
        {
            db.SubmitChanges();
        }

    }
}
