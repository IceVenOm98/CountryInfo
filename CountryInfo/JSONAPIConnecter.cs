using Newtonsoft.Json;
using System;
using System.Net;

namespace CountryInfo
{
    internal class JSONAPIConnecter : Interfaces.IAPIConnecter
    {
        public int mode { get; set; }
        public Country GetCountry(string url)
        {
            string response = GetStringFromUrl(url);
            switch (mode)
            {
                case 0:
                    response = EditStringFromRestcountries(response);
                    break;
                case 1:
                    response = EditStringFromHtmlweb(response);
                    break;
                case 2:
                    response = EditStringFromIpgeolocationapi(response);
                    break;
                default: 
                    response = EditStringFromRestcountries(response);
                    break;
            }

            return DeserializeJSON(response);
            
        }

        private string GetStringFromUrl(string url)
        {
            var webClient = new WebClient();
            try
            {
                var response = webClient.DownloadString(url);
                return response;
            }
            catch (WebException ex)
            {
                throw new Exception("Проблема с подключением к внешнему API, проверьте адрес или название страны.", ex);
            }
            catch (Newtonsoft.Json.JsonSerializationException ex)
            {
                throw new Exception("Проблема с парсингом JSON. API не сопадает с ожидаемым.", ex);
            }
            catch (Exception ex)
            {
                throw;// new Exception("Полученный формат не является JSON-объектом.", ex);
            }
        }
        private string EditStringFromRestcountries(string response)
        {
            return response = response.Substring(1, response.Length - 2);
        }
        private string EditStringFromHtmlweb(string response)
        {

            response = response.Replace("\"country\":{", "");
            response = response.Replace("},\"english", ",\"english");
            response = response.Replace("location", "region");
            response = response.Replace("iso", "numericCode");
            response = response.Replace("\\/", "/");
            response = response.Substring(0, response.Length - 1) + ",\"area\":0.0}";
            response = response.Substring(0, response.Length - 1) + ",\"population\":0}";
            response = response.Substring(0, response.Length - 1) + ",\"capital\":\"none\"}";
            return response;
        }
        private string EditStringFromIpgeolocationapi(string response)
        {
            response = response.Substring(0, response.Length - 1) + ",\"area\":0.0}";
            response = response.Substring(0, response.Length - 1) + ",\"population\":0}";
            response = response.Substring(0, response.Length - 1) + ",\"capital\":\"unknown\"}";
            response = response.Replace("number", "numericCode");
            return response;
        }

        private Country DeserializeJSON(string response)
        {
            response = response.Replace("'", "*");//избавляемся от ' в названиях
            response = response.Replace("\"", "'");//заменяем двойные кавычки на одинарные
            Country country = JsonConvert.DeserializeObject<Country>(response);
            return country;
        }
    }
}
