using System;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    public class PriceItem
    {
        public PriceItem(int id, DateTime lastUpdate, string code, string name, string priceBase,
            string priceBaseWithoutVat, string priceBaseWithVat)
        {
            Id = id;
            LastUpdate = lastUpdate;
            Code = code;
            Name = name;
            PriceBase = priceBase;
            PriceBaseWithoutVat = priceBaseWithoutVat;
            PriceBaseWithVat = priceBaseWithVat;
        }
        
        [JsonProperty("id")] public int Id { get; }
        [JsonProperty("lastUpdate")] public DateTime LastUpdate { get; }
        [JsonProperty("kod")] public string Code { get; }
        [JsonProperty("nazev")] public string Name { get; }
        [JsonProperty("cenaZakl")] public string PriceBase { get; }
        [JsonProperty("cenaZaklBezDph")] public string PriceBaseWithoutVat { get; }
        [JsonProperty("cenaZaklVcDph")] public string PriceBaseWithVat { get; }
    }
}