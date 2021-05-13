
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PurchaseData.Indicadores
{
    public class IndicadorUf
    {
        [JsonPropertyName("UFs")]
        public List<Indicador> Uf { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]
        private Uri UriBase { get; }
        private string ApiKey { get; }

        public IndicadorUf()
        {
            WebClient = new WebClient();
            ApiKey = Properties.Resources.ApikeySBIF;
            UriBase = new Uri(Properties.Resources.BaseSBIF);
        }

        public async Task<IndicadorUf> GetPosterior(DateTime d)
        {
            try
            {
                using (WebClient)
                {
                    Uri uri = new Uri(UriBase, $"uf/posteriores/{d.Year}/{d.Month}/dias/{d.Day}?apikey={ApiKey}&formato=json");
                    string response = await WebClient.DownloadStringTaskAsync(uri);
                    if (response != null)
                    {
                        var r = JsonSerializer.Deserialize<IndicadorUf>(response);
                        if (r != null)
                        {
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