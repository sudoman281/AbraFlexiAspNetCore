using Newtonsoft.Json;

namespace AbraFlexiAspNetCore.Models.Create
{
    /// <summary>
    /// Default Abra request wrapper
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class AbraRequest<T>
    {
        /// <summary>
        /// Default Abra request wrapper
        /// </summary>
        /// <param name="data">Request data</param>
        public AbraRequest(T data)
        {
            Data = data;
        }

        /// <summary>
        /// Request data
        /// </summary>
        [JsonProperty("winstrom")] public T Data { get; }
    }
}