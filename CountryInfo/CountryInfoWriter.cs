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
        public String countryInfo(Country country)
        {

            string countryInfo = "";
            countryInfo += "Title:    " + country.name + "\n";
            countryInfo += "Code:    " + country.numericCode + "\n";
            countryInfo += "Capital:    " + country.capital + "\n";
            countryInfo += "Area:    " + country.area + "\n";
            countryInfo += "Population:    " + country.population + "\n";
            countryInfo += "Region:    " + country.region + "\n";
            countryInfo += "=======================================" + "\n";
            return countryInfo;
        }
    }
}
