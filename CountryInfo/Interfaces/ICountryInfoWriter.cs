using System;

namespace CountryInfo.Interfaces
{
    /// <summary>
    /// Интерфейс для классов, отвечающих за вывод информации о стране
    /// </summary>
    internal interface ICountryInfoWriter
    {
        /// <summary>
        /// Собирает информацию о стране
        /// </summary>
        /// <param name="c"></param>
        /// Принимает на вход объект страну
        /// <returns></returns>
        /// Возвращает строку, содержащую всю информацию о стране
        String CountryInfo(Country c);
    }
}
