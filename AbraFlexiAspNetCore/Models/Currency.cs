using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    /// <summary>
    /// Currency
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Currency
        /// </summary>
        /// <param name="id">Id of the currency</param>
        /// <param name="code">Code of the currency</param>
        /// <param name="name">Name of the currency</param>
        public Currency(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        /// <summary>
        /// Id of the currency
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
        /// <summary>
        /// Code of the currency
        /// </summary>
        [JsonProperty("kod")] public string Code { get; }
        /// <summary>
        /// Name of the currency
        /// </summary>
        [JsonProperty("nazev")] public string Name { get; }
    }
}