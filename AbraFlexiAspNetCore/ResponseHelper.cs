using Newtonsoft.Json.Linq;

namespace AbraFlexiAspNetCore
{
    public static class ResponseHelper
    {
        public static T ToObject<T>(this string json)
        {
            var standardObject = JObject.Parse(json);
            var winstrom = standardObject["winstrom"];
            var result = winstrom[((JProperty)winstrom.Last).Name].ToObject<T>();
            return result;
        }
    }
}