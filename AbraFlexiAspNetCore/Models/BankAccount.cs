using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    /// <summary>
    /// Bank account
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Bank account
        /// </summary>
        /// <param name="id">Id of the bank account</param>
        /// <param name="code">Code of the bank account</param>
        /// <param name="name">Name of the bank account</param>
        /// <param name="currency">Currency of the bank account</param>
        public BankAccount(int id, string code, string name, string currency)
        {
            Id = id;
            Code = code;
            Name = name;
            Currency = currency;
        }

        /// <summary>
        /// Id of the bank account
        /// </summary>
        [JsonProperty("id")] public int Id { get; }
        /// <summary>
        /// Code of the bank account
        /// </summary>
        [JsonProperty("kod")] public string Code { get; }
        /// <summary>
        /// Name of the bank account
        /// </summary>
        [JsonProperty("nazev")] public string Name { get; }
        /// <summary>
        /// Currency of the bank account
        /// </summary>
        [JsonProperty("mena")] public string Currency { get; }
    }
}