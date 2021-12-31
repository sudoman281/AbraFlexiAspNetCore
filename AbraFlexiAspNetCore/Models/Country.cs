using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    /// <summary>
    /// Currency
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Currency
        /// </summary>
        /// <param name="id">Id of the country</param>
        /// <param name="code">Code of the country</param>
        /// <param name="name">Name of the country</param>
        public Country(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        /// <summary>
        /// Id of the country
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
        /// <summary>
        /// Code of the country
        /// </summary>
        [JsonProperty("kod")] public string Code { get; }
        /// <summary>
        /// Name of the country
        /// </summary>
        [JsonProperty("nazev")] public string Name { get; }
    }
}