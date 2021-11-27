using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models
{
    public class Currency
    {
        public Currency(int id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        [JsonProperty("id")] public int Id { get; }
        [JsonProperty("kod")] public string Code { get; }
        [JsonProperty("nazev")] public string Name { get; }
    }
}