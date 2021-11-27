using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    public class BankAccount
    {
        public BankAccount(int id, string code, string name, string currency)
        {
            Id = id;
            Code = code;
            Name = name;
            Currency = currency;
        }

        [JsonProperty("id")] public int Id { get; }
        [JsonProperty("kod")] public string Code { get; }
        [JsonProperty("nazev")] public string Name { get; }
        [JsonProperty("mena")] public string Currency { get; }
    }
}