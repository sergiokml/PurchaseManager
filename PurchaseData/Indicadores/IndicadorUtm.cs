
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PurchaseData.Indicadores
{
    public class IndicadorUtm
    {
        [JsonPropertyName("UTMs")]
        public List<Indicador> Utm { get; set; }

        private WebClient WebClient { get; set; } // CON PRIVATE NO ES NECESARIO [JsonIgnore]
        private string ApiKey { get; }
        public IndicadorUtm()
        {
            WebClient = new WebClient() { BaseAddress = Properties.Resources.BaseSBIF };
            ApiKey = Properties.Resources.ApikeySBIF;
        }

        public Indicador GetToday()
        {
            using (WebClient)
            {
                var response = WebClient.DownloadString($"utm?apikey={ApiKey}&formato=json");
                var r = JsonSerializer.Deserialize<IndicadorUtm>(response);
                if (r.Utm.Count > 0)
                {
                    return r.Utm[0];
                }
                return null;
            }
        }
        public IndicadorUtm GetPosterior(DateTime d)
        {
            try
            {
                using (WebClient)
                {
                    var url = $"utm/posteriores/{d.Year}/{d.Month}/?apikey={ApiKey}&formato=json";
                    var response = WebClient.DownloadString(url);
                    var r = JsonSerializer.Deserialize<IndicadorUtm>(response);
                    if (r != null)
                    {
                        return r;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}