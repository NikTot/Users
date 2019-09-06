using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using System.Net;

namespace SAU.Services
{
    public class ClearTextService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public string CleanUp(string text, string url)
        {            
            try
            {
                var result = string.Empty;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(new { value = text });
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Ошибка при получении данных из сервиса очистки текста");
                return "нет данных";
            }

            
        }
    }
}