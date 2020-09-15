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
        private IAPIConnecter apiConnecter;
        public Form1()
        {
            InitializeComponent();
            dBConnecter = new DBConnecter(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Countries;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            countryWriter = new CountryInfoWriter();
            apiConnecter = new JSONAPIConnecter();
            apiConnecter.mode = 0;
            countries = new List<Country>();
            showCountriesInDB();
        }

        /// <summary>
        /// Запрос информации по API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_GetCountryByAPI(object sender, EventArgs e)
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
        private void button2_SaveToDB(object sender, EventArgs e)
        {
            Country country = countries.Last();

            //Проверка столицы
            int cityId;
            try
            {
                cityId = dBConnecter.getCityByTitle(country.capital).Id;
            }
            catch (NullReferenceException ex)
            {
                Cities capital = new Cities { Title = country.capital };
                dBConnecter.addCity(capital);
                cityId = capital.Id;
            }

            //Проверка региона
            int RegionId;
            try
            {
                RegionId = dBConnecter.getRegionByTitle(country.region).Id;
            }
            catch (NullReferenceException ex)
            {
                Regions region = new Regions { Title = country.region };
                dBConnecter.addRegion(region);
                RegionId = region.Id;
            }

            //проверка кода страны
            try
            {
                Countries c = dBConnecter.getCountryByCode(country.numericCode);
                //изменение инфы старой страны
                c.Area = countries.Last().area;
                c.Capital = cityId;
                c.Population = countries.Last().population;
                c.Region = RegionId;
                c.Title = countries.Last().name;
                dBConnecter.updateDB();
            }
            catch (NullReferenceException ex)
            {
                //добавление в бд новой страны
                Countries country1 = new Countries
                {
                    Title = country.name,
                    Code = country.numericCode,
                    Capital = cityId,
                    Area = country.area,
                    Population = country.population,
                    Region = RegionId
                };
                // добавляем в таблицу 
                dBConnecter.addCountry(country1);
            }

            showCountriesInDB();

        }
        /// <summary>
        /// Обновляет вывод всех стран из БД
        /// </summary>
        private void showCountriesInDB()
        {
            updateListOfCountries();
            richTextBox2.Text = "";
            foreach (Country c in countries)
            {
                richTextBox2.Text += countryWriter.countryInfo(c);
            }
        }
        /// <summary>
        /// Обновляет список стран из БД
        /// </summary>
        private void updateListOfCountries()
        {
            countries.Clear();
            foreach (Countries c in dBConnecter.getAllCountries())
            {

                try
                {
                    Country country = new Country();
                    country.name = c.Title;
                    country.numericCode = c.Code;
                    country.population = c.Population;
                    country.area = c.Area;
                    country.capital = dBConnecter.getCityById(c.Capital).Title;
                    country.region = dBConnecter.getRegionById(c.Region).Title;
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
        private void restcountriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            APIurl = "https://restcountries.eu/rest/v2/name/";
            apiConnecter.mode = 0;
            foreach (ToolStripMenuItem m in aPIToolStripMenuItem.DropDownItems)
            {
                m.Checked = false;
            }
            restcountriesToolStripMenuItem.Checked = true;

        }

        /// <summary>
        /// Выбор API#2, временно отключен из-за ограничений сервера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void htmlwebToolStripMenuItem_Click(object sender, EventArgs e)
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
        private void ipgeolocationapiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            APIurl = "https://api.ipgeolocationapi.com/countries/";
            apiConnecter.mode = 2;
            foreach (ToolStripMenuItem m in aPIToolStripMenuItem.DropDownItems)
            {
                m.Checked = false;
            }
            ipgeolocationapiToolStripMenuItem.Checked = true;
        }
    }
}
