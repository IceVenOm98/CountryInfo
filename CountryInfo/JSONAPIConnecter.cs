using Newtonsoft.Json;
using System;
using System.Net;

namespace CountryInfo
{
    internal class JSONAPIConnecter : Interfaces.IAPIConnecter
    {
        public int mode { get; set; }
        public Country GetCountry(String url)
        {
            var webClient = new WebClient();
            try
            {
                var response = webClient.DownloadString(url);
                if (mode == 0)
                {
                    response = response.Substring(1, response.Length - 2);
                }
                if (mode == 1)
                {
                    response = response.Replace("\"country\":{", "");
                    response = response.Replace("},\"english", ",\"english");
                    response = response.Replace("location", "region");
                    response = response.Replace("iso", "numericCode");
                    response = response.Replace("\\/", "/");
                    response = response.Substring(0, response.Length - 1) + ",\"area\":0.0}";
                    response = response.Substring(0, response.Length - 1) + ",\"population\":0}";
                    response = response.Substring(0, response.Length - 1) + ",\"capital\":\"none\"}";

                }
                if (mode == 2)
                {
                    response = response.Substring(0, response.Length - 1) + ",\"area\":0.0}";
                    response = response.Substring(0, response.Length - 1) + ",\"population\":0}";
                    response = response.Substring(0, response.Length - 1) + ",\"capital\":\"unknown\"}";
                    response = response.Replace("number", "numericCode");
                }
                response = response.Replace("'", "*");
                response = response.Replace("\"", "'");
                Country country = JsonConvert.DeserializeObject<Country>(response);
                return country;
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
    }
}
