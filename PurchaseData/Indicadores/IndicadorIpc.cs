
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurchaseData.Indicadores
{
    public class IndicadorIpc
    {
        [JsonPropertyName("IPCs")]
        public List<Indicador> Ipc { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]
        private string ApiKey { get; }
        public IndicadorIpc()
        {
            WebClient = new WebClient() { BaseAddress = Properties.Resources.BaseSBIF };
            ApiKey = Properties.Resources.ApikeySBIF;
        }

        public Indicador GetToday()
        {
            using (WebClient)
            {
                var response = WebClient.DownloadString($"ipc?apikey={ApiKey}&formato=json");
                var r = JsonSerializer.Deserialize<IndicadorIpc>(response);
                if (r.Ipc.Count > 0)
                {
                    return r.Ipc[0];
                }
                return null;
            }
        }
    }
}