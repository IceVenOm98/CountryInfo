using System;

namespace CountryInfo.Interfaces
{
    internal interface IAPIConnecter
    {
        /// <summary>
        /// Переключатель номеров API
        /// </summary>
        int mode { get; set; }
        /// <summary>
        /// Получает страну по ссылкке
        /// </summary>
        /// <param name="url"></param>
        /// Ссылка на API
        /// <returns></returns>
        Country GetCountry(String url);
    }
}
