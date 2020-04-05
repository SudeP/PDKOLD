using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.Tool
{
    public class JsonObject<T> where T : class, new()
    {
        public static T FromJson(string json) => JsonConvert.DeserializeObject<T>(json);
    }
    public static class JsonObjectExtension
    {
        [Obsolete("inheritance PDK.Tool.JsonObject<T> for you class")]
        public static T FromJson<T>(this T _, string json) where T : class, new() => JsonConvert.DeserializeObject<T>(json);
        public static string ToJson<T>(this T _) where T : class, new() => JsonConvert.SerializeObject(_);
    }
}
