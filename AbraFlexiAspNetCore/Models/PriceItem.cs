using System;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    public class PriceItem
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("lastUpdate")]
        public DateTime LastUpdate;

        [JsonProperty("kod")]
        public string Code;

        [JsonProperty("nazev")]
        public string Name;

        [JsonProperty("cenaZakl")]
        public string PriceBase;

        [JsonProperty("cenaZaklBezDph")]
        public string PriceBaseWithoutVat;

        [JsonProperty("cenaZaklVcDph")]
        public string PriceBaseWithVat;

        public PriceItem(string id, DateTime lastUpdate, string code, string name, string priceBase, string priceBaseWithoutVat, string priceBaseWithVat)
        {
            Id = id;
            LastUpdate = lastUpdate;
            Code = code;
            Name = name;
            PriceBase = priceBase;
            PriceBaseWithoutVat = priceBaseWithoutVat;
            PriceBaseWithVat = priceBaseWithVat;
        }
    }
}