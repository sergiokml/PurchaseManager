
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PurchaseData.Indicadores
{
    public class IndicadorUtm
    {
        [JsonPropertyName("UTMs")]
        public List<Indicador> Utm { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]
        private string ApiKey { get; }
        private Uri UriBase { get; }

        public IndicadorUtm(DataModel.ConfigApp dbConfig)
        {
            WebClient = new WebClient();
            ApiKey = dbConfig.ApikeySBIF;
            UriBase = new Uri(dbConfig.BaseSBIF);
        }


        public IndicadorUtm()
        {

        }
        public async Task<IndicadorUtm> GetPosterior(DateTime d)
        {
            try
            {
                using (WebClient)
                {
                    Uri uri = new Uri(UriBase, $"utm/posteriores/{d.Year}/{d.Month}?apikey={ApiKey}&formato=json");
                    string response = await WebClient.DownloadStringTaskAsync(uri);
                    if (response != null)
                    {
                        var r = JsonSerializer.Deserialize<IndicadorUtm>(response);
                        if (r != null)
                        {
                            r.Utm.RemoveAll(c => Convert.ToDateTime(c.Fecha) > DateTime.Now);
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
