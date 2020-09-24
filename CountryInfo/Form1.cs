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
        private List<Country> Countries;
        private DBConnecter DBConnecter;
        private ICountryInfoWriter CountryWriter;
        private IAPIConnecter APIConnecter = new JSONAPIConnecter();
        public Form1()
        {
            InitializeComponent();
            DBConnecter = new DBConnecter();
            CountryWriter = new CountryInfoWriter();
            APIConnecter.Mode = 0;
            Countries = new List<Country>();
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
                Country country = APIConnecter.GetCountry(url);
                richTextBox1.Text = CountryWriter.CountryInfo(country);
                Countries.Add(country);
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
            if (!DBConnecter.IsSetConfig())
                MessageBox.Show("БД не подключена. Выберите конфигуарционный файл.");
            else
            {
                Country country = Countries.Last();

                //Проверка столицы
                int cityId;
                try
                {
                    cityId = DBConnecter.GetCityByTitle(country.Capital).Id;
                }
                catch (NullReferenceException)
                {
                    Cities capital = new Cities { Title = country.Capital };
                    DBConnecter.AddCity(capital);
                    cityId = capital.Id;
                }

                //Проверка региона
                int RegionId;
                try
                {
                    RegionId = DBConnecter.GetRegionByTitle(country.Region).Id;
                }
                catch (NullReferenceException)
                {
                    Regions region = new Regions { Title = country.Region };
                    DBConnecter.AddRegion(region);
                    RegionId = region.Id;
                }

                //проверка кода страны
                try
                {
                    Countries c = DBConnecter.GetCountryByCode(country.NumericCode);
                    //изменение инфы старой страны
                    c.Area = Countries.Last().Area;
                    c.Capital = cityId;
                    c.Population = Countries.Last().Population;
                    c.Region = RegionId;
                    c.Title = Countries.Last().Name;
                    DBConnecter.UpdateDB();
                }
                catch (System.NullReferenceException)
                {
                    //если страна не найдена, добавление ее в бд 
                    Countries country1 = new Countries
                    {
                        Title = country.Name,
                        Code = country.NumericCode,
                        Capital = cityId,
                        Area = country.Area,
                        Population = country.Population,
                        Region = RegionId
                    };
                    DBConnecter.AddCountry(country1);
                }
                ShowCountriesInDB();
            }


        }
        /// <summary>
        /// Обновляет вывод всех стран из БД
        /// </summary>
        private void ShowCountriesInDB()
        {
            if (DBConnecter.IsSetConfig())
            {
                UpdateListOfCountries();
                richTextBox2.Text = "";
                foreach (Country c in Countries)
                {
                    richTextBox2.Text += CountryWriter.CountryInfo(c);
                }
            }
        }
        /// <summary>
        /// Обновляет список стран из БД
        /// </summary>
        private void UpdateListOfCountries()
        {
            Countries.Clear();
            foreach (Countries c in DBConnecter.GetAllCountries())
            {
                try
                {
                    Country country = new Country
                    {
                        Name = c.Title,
                        NumericCode = c.Code,
                        Population = c.Population,
                        Area = c.Area,
                        Capital = DBConnecter.GetCityById(c.Capital).Title,
                        Region = DBConnecter.GetRegionById(c.Region).Title
                    };
                    Countries.Add(country);
                }
                catch (NullReferenceException)
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
            APIConnecter.Mode = 0;
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
            APIConnecter.Mode = 1;
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
            APIConnecter.Mode = 2;
            foreach (ToolStripMenuItem m in aPIToolStripMenuItem.DropDownItems)
            {
                m.Checked = false;
            }
            ipgeolocationapiToolStripMenuItem.Checked = true;
            label1.Text = "Enter country code (ru, uk, etc.)";
        }

        private void ChooseConfigFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "..\\..\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (!DBConnecter.SetConfigs(openFileDialog.FileName))
                        {
                            MessageBox.Show("Подключить БД не удалось, выберите другой файл");
                            return;
                        }
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Конфигурация неверная, выберите другой файл");
                        ChooseConfigFileToolStripMenuItem_Click(sender, e);
                    }
                }
                ShowCountriesInDB();
            }
        }
    }
}
