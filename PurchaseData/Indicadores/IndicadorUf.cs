
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurchaseData.Indicadores
{
    public class IndicadorUf
    {
        [JsonPropertyName("UFs")]
        public List<Indicador> Uf { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]
        private string ApiKey { get; }
        public IndicadorUf()
        {
            WebClient = new WebClient() { BaseAddress = Properties.Resources.BaseSBIF };
            ApiKey = Properties.Resources.ApikeySBIF;
        }

        public Indicador GetToday()
        {
            using (WebClient)
            {
                var response = WebClient.DownloadString($"uf?apikey={ApiKey}&formato=json");
                var r = JsonSerializer.Deserialize<IndicadorUf>(response);
                if (r.Uf.Count > 0)
                {
                    return r.Uf[0];
                }
                return null;
            }
        }
        public IndicadorUf GetPosterior(DateTime d)
        {
            using (WebClient)
            {
                var url = $"uf/posteriores/{d.Year}/{d.Month}/dias/{d.Day}?apikey={ApiKey}&formato=json";
                var response = WebClient.DownloadString(url);
                var r = JsonSerializer.Deserialize<IndicadorUf>(response);
                if (r != null)
                {
                    return r;
                }
                return null;
            }
        }
    }
}