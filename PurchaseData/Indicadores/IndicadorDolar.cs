using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurchaseData.Indicadores
{
    public class IndicadorDolar
    {
        [JsonPropertyName("Dolares")]
        public List<Indicador> Dolar { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]

        private string ApiKey { get; }

        public IndicadorDolar()
        {
            WebClient = new WebClient() { BaseAddress = Properties.Resources.BaseSBIF };
            ApiKey = Properties.Resources.ApikeySBIF;
        }

        public Indicador GetToday()
        {
            using (WebClient)
            {
                var response = WebClient.DownloadString($"dolar?apikey={ApiKey}&formato=json");
                var r = JsonSerializer.Deserialize<IndicadorDolar>(response);
                if (r.Dolar.Count > 0)
                {
                    return r.Dolar[0];
                }
                return null;
            }
        }

        public Indicador GetDay(DateTime d)
        {
            using (WebClient)
            {
                var url = $"dolar/{d.Year}/{d.Month}/dias/{d.Day}?apikey={ApiKey}&formato=json";
                var response = WebClient.DownloadString(url);
                var r = JsonSerializer.Deserialize<IndicadorDolar>(response);
                if (r.Dolar.Count > 0)
                {
                    return r.Dolar[0];
                }
                return null;
            }
        }

        public IndicadorDolar GetPosterior(DateTime d)
        {
            using (WebClient)
            {
                var url = $"dolar/posteriores/{d.Year}/{d.Month}/dias/{d.Day}?apikey={ApiKey}&formato=json";
                var response = WebClient.DownloadString(url);
                var r = JsonSerializer.Deserialize<IndicadorDolar>(response);
                if (r != null)
                {
                    return r;
                }
                return null;
            }
        }
    }
}
