using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    public class AbraRequest<T>
    {
        public AbraRequest(T data)
        {
            Data = data;
        }

        [JsonProperty("winstrom")] public T Data { get; }
    }
}