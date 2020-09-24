using CountryInfo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CountryInfo
{
    public partial class Form1 : Form
    {
        private string APIurl = "https://restcountries.eu/rest/v2/name/";
        private List<Country> countries;
        private DBConnecter dBConnecter;
        private ICountryInfoWriter countryWriter;
        private IAPIConnecter apiConnecter = new JSONAPIConnecter();
        public Form1()
        {
            InitializeComponent();
            dBConnecter = new DBConnecter();
            countryWriter = new CountryInfoWriter();
            apiConnecter.mode = 0;
            countries = new List<Country>();
            ShowCountriesInDB();
        }


        /// <summary>
        /// Запрос информации по API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_GetCountryByAPI(object sender, EventArgs e)
        {

            string url = APIurl + textBox1.Text;
            try
            {
                Country country = apiConnecter.GetCountry(url);
                richTextBox1.Text = countryWriter.countryInfo(country);
                countries.Add(country);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        /// <summary>
        /// Сохранение информации в БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_SaveToDB(object sender, EventArgs e)
        {
            Country country = countries.Last();

            //Проверка столицы
            int cityId;
            try
            {
                cityId = dBConnecter.GetCityByTitle(country.capital).Id;
            }
            catch (NullReferenceException)
            {
                Cities capital = new Cities { Title = country.capital };
                dBConnecter.AddCity(capital);
                cityId = capital.Id;
            }

            //Проверка региона
            int RegionId;
            try
            {
                RegionId = dBConnecter.GetRegionByTitle(country.region).Id;
            }
            catch (NullReferenceException ex)
            {
                Regions region = new Regions { Title = country.region };
                dBConnecter.AddRegion(region);
                RegionId = region.Id;
            }

            //проверка кода страны
            try
            {
                Countries c = dBConnecter.GetCountryByCode(country.numericCode);
                //изменение инфы старой страны
                c.Area = countries.Last().area;
                c.Capital = cityId;
                c.Population = countries.Last().population;
                c.Region = RegionId;
                c.Title = countries.Last().name;
                dBConnecter.UpdateDB();
            }
            catch (System.NullReferenceException)
            {
                //если страна не найдена, добавление ее в бд 
                Countries country1 = new Countries();
                country1.Title = country.name;
                country1.Code = country.numericCode;
                country1.Capital = cityId;
                country1.Area = country.area;
                country1.Population = country.population;
                country1.Region = RegionId;
                dBConnecter.AddCountry(country1);
            }

            ShowCountriesInDB();

        }
        /// <summary>
        /// Обновляет вывод всех стран из БД
        /// </summary>
        private void ShowCountriesInDB()
        {
            UpdateListOfCountries();
            richTextBox2.Text = "";
            foreach (Country c in countries)
            {
                richTextBox2.Text += countryWriter.countryInfo(c);
            }
        }
        /// <summary>
        /// Обновляет список стран из БД
        /// </summary>
        private void UpdateListOfCountries()
        {
            countries.Clear();
            foreach (Countries c in dBConnecter.GetAllCountries())
            {

                try
                {
                    Country country = new Country
                    {
                        name = c.Title,
                        numericCode = c.Code,
                        population = c.Population,
                        area = c.Area,
                        capital = dBConnecter.GetCityById(c.Capital).Title,
                        region = dBConnecter.GetRegionById(c.Region).Title
                    };
                    countries.Add(country);
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show("Проблема с отображением страны " + c.Title);
                }

            }
        }
        /// <summary>
        /// Выбор API#1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RestcountriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APIurl = "https://restcountries.eu/rest/v2/name/";
            apiConnecter.mode = 0;
            foreach (ToolStripMenuItem m in aPIToolStripMenuItem.DropDownItems)
            {
                m.Checked = false;
            }
            restcountriesToolStripMenuItem.Checked = true;
            label1.Text = "Enter country title";

        }

        /// <summary>
        /// Выбор API#2, временно отключен из-за ограничений сервера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HtmlwebToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APIurl = "https://htmlweb.ru/geo/api.php?json&info&charset=utf-8&short&country=";
            apiConnecter.mode = 1;
            foreach (ToolStripMenuItem m in aPIToolStripMenuItem.DropDownItems)
            {
                m.Checked = false;
            }
            htmlwebToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// Выбор API#3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IpgeolocationapiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            APIurl = "https://api.ipgeolocationapi.com/countries/";
            apiConnecter.mode = 2;
            foreach (ToolStripMenuItem m in aPIToolStripMenuItem.DropDownItems)
            {
                m.Checked = false;
            }
            ipgeolocationapiToolStripMenuItem.Checked = true;
            label1.Text = "Enter country code (ru, uk, etc.)";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
