
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PurchaseData.Indicadores
{
    public class IndicadorIpc
    {
        [JsonPropertyName("IPCs")]
        public List<Indicador> Ipc { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]
        private string ApiKey { get; }
        private Uri UriBase { get; }

        public IndicadorIpc(DataModel.ConfigApp dbConfig)
        {
            WebClient = new WebClient();
            ApiKey = dbConfig.ApikeySBIF;
            UriBase = new Uri(dbConfig.BaseSBIF);
        }

        public IndicadorIpc()
        {

        }
        public async Task<IndicadorIpc> GetPosterior(DateTime d)
        {
            try
            {
                using (WebClient)
                {
                    Uri uri = new Uri(UriBase, $"ipc/posteriores/{d.Year}/{d.Month}/?apikey={ApiKey}&formato=json");
                    string response = await WebClient.DownloadStringTaskAsync(uri);
                    if (response != null)
                    {
                        var r = JsonSerializer.Deserialize<IndicadorIpc>(response);
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
