using Newtonsoft.Json.Linq;

namespace AbraFlexiAspNetCore
{
    /// <summary>
    /// Response helper
    /// </summary>
    public static class ResponseHelper
    {
        /// <summary>
        /// Transfers the default Abra response to objects
        /// </summary>
        /// <param name="json">JSON representation of the data</param>
        /// <typeparam name="T">Type of the output object</typeparam>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            var standardObject = JObject.Parse(json);
            var winstrom = standardObject["winstrom"];
            var result = winstrom[((JProperty)winstrom.Last).Name].ToObject<T>();
            return result;
        }
    }
}