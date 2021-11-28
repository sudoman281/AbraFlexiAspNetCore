using System;
using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    /// <summary>
    /// Price list item
    /// </summary>
    public class PriceItem
    {
        /// <summary>
        /// Price list item
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="lastUpdate">Date of the last update</param>
        /// <param name="code">Code</param>
        /// <param name="name">Name</param>
        /// <param name="priceBase">Base price</param>
        /// <param name="priceBaseWithoutVat">Base price without VAT</param>
        /// <param name="priceBaseWithVat">Base price with VAT</param>
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
        
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
        /// <summary>
        /// Date of the last update
        /// </summary>
        [JsonProperty("lastUpdate")] public DateTime LastUpdate { get; }
        /// <summary>
        /// Code
        /// </summary>
        [JsonProperty("kod")] public string Code { get; }
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("nazev")] public string Name { get; }
        /// <summary>
        /// Base price
        /// </summary>
        [JsonProperty("cenaZakl")] public string PriceBase { get; }
        /// <summary>
        /// Base price without VAT
        /// </summary>
        [JsonProperty("cenaZaklBezDph")] public string PriceBaseWithoutVat { get; }
        /// <summary>
        /// Base price with VAT
        /// </summary>
        [JsonProperty("cenaZaklVcDph")] public string PriceBaseWithVat { get; }
    }
}