using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using PurchaseData.DataModel;

namespace PurchaseData.Indicadores
{
    public class IndicadorDolar
    {
        [JsonPropertyName("Dolares")]
        public List<Indicador> Dolar { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]

        private string ApiKey { get; }

        private Uri UriBase { get; }

        public IndicadorDolar(ConfigApp dbConfig)
        {
            WebClient = new WebClient();
            ApiKey = dbConfig.ApikeySBIF;
            UriBase = new Uri(dbConfig.BaseSBIF);
        }
        public IndicadorDolar()
        {

        }

        public async Task<IndicadorDolar> GetPosterior(DateTime d)
        {
            try
            {
                using (WebClient)
                {
                    Uri uri = new Uri(UriBase, $"dolar/posteriores/{d.Year}/{d.Month}/dias/{d.Day}?apikey={ApiKey}&formato=json");
                    string response = await WebClient.DownloadStringTaskAsync(uri);
                    if (response != null)
                    {
                        var r = JsonSerializer.Deserialize<IndicadorDolar>(response);
                        if (r != null)
                        {
                            r.Dolar.RemoveAll(c => Convert.ToDateTime(c.Fecha) > DateTime.Now);
                            return r;
                        }
                    }
                }
            }
            catch (WebException)
            {
                return null;
            }
            return null;
        }
    }
}
