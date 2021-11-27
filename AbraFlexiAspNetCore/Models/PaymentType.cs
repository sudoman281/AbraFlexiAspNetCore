using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    public class PaymentType
    {
        public PaymentType(int id, string code, string name, int? currency)
        {
            Id = id;
            Code = code;
            Name = name;
            Currency = currency;
        }

        [JsonProperty("id")] public int Id { get; }
        [JsonProperty("kod")] public string Code { get; }
        [JsonProperty("nazev")] public string Name { get; }
        [JsonProperty("mena")] public int? Currency { get; }
    }
}