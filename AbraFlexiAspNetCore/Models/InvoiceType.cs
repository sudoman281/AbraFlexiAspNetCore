using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    /// <summary>
    /// Invoice type
    /// </summary>
    public class InvoiceType
    {
        /// <summary>
        /// Invoice type
        /// </summary>
        /// <param name="id">Id of the invoice type</param>
        /// <param name="code">Code of the invoice type</param>
        /// <param name="name">Name of the invoice type</param>
        /// <param name="currency">Currency of the invoice type</param>
        public InvoiceType(int id, string code, string name, string currency)
        {
            Id = id;
            Code = code;
            Name = name;
            Currency = currency;
        }

        /// <summary>
        /// Id of the invoice type
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
        /// <summary>
        /// Code of the invoice type
        /// </summary>
        [JsonProperty("kod")] public string Code { get; }
        /// <summary>
        /// Name of the invoice type
        /// </summary>
        [JsonProperty("nazev")] public string Name { get; }
        /// <summary>
        /// Currency of the invoice type
        /// </summary>
        [JsonProperty("mena")] public string Currency { get; }
    }
}