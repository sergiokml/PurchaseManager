
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurchaseData.Indicadores
{
    public class IndicadorEuro
    {
        [JsonPropertyName("Euros")]
        public List<Indicador> Euro { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]
        private string ApiKey { get; }
        public IndicadorEuro()
        {
            WebClient = new WebClient() { BaseAddress = Properties.Resources.BaseSBIF };
            ApiKey = Properties.Resources.ApikeySBIF;
        }

        public Indicador GetToday()
        {
            using (WebClient)
            {
                var response = WebClient.DownloadString($"euro?apikey={ApiKey}&formato=json");
                var r = JsonSerializer.Deserialize<IndicadorEuro>(response);
                if (r.Euro.Count > 0)
                {
                    return r.Euro[0];
                }
                return null;
            }
        }
        public IndicadorEuro GetPosterior(DateTime d)
        {
            using (WebClient)
            {
                var url = $"euro/posteriores/{d.Year}/{d.Month}/dias/{d.Day}?apikey={ApiKey}&formato=json";
                var response = WebClient.DownloadString(url);
                var r = JsonSerializer.Deserialize<IndicadorEuro>(response);
                if (r != null)
                {
                    return r;
                }
                return null;
            }
        }
    }
}