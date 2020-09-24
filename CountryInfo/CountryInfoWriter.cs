using System;

namespace CountryInfo
{
    internal class CountryInfoWriter : Interfaces.ICountryInfoWriter
    {
        /// <summary>
        /// Создает строку, содержащую информацию о стране
        /// </summary>
        /// <param name="country"></param>
        /// Принимает на вход объект-страну
        /// <returns></returns>
        public string CountryInfo(Country country)
        {

            string countryInfo = "";
            countryInfo += "Title:    " + country.Name + "\n";
            countryInfo += "Code:    " + country.NumericCode + "\n";
            countryInfo += "Capital:    " + country.Capital + "\n";
            countryInfo += "Area:    " + country.Area + "\n";
            countryInfo += "Population:    " + country.Population + "\n";
            countryInfo += "Region:    " + country.Region + "\n";
            countryInfo += "=======================================" + "\n";
            return countryInfo;
        }
    }
}
