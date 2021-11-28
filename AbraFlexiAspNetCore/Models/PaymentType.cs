using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    /// <summary>
    /// Type of the payment
    /// </summary>
    public class PaymentType
    {
        /// <summary>
        /// Type of the payment (Card, cash, ...)
        /// </summary>
        /// <param name="id">Id of the payment type</param>
        /// <param name="code">Code of the payment type</param>
        /// <param name="name">Name of the payment type</param>
        /// <param name="currency">Currency of the payment type</param>
        public PaymentType(int id, string code, string name, int? currency)
        {
            Id = id;
            Code = code;
            Name = name;
            Currency = currency;
        }

        /// <summary>
        /// Id of the payment type
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
        /// <summary>
        /// Code of the payment type
        /// </summary>
        [JsonProperty("kod")] public string Code { get; }
        /// <summary>
        /// Name of the payment type
        /// </summary>
        [JsonProperty("nazev")] public string Name { get; }
        /// <summary>
        /// Currency of the payment type
        /// </summary>
        [JsonProperty("mena")] public int? Currency { get; }
    }
}