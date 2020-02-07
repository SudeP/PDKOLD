using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.Tool
{
    public static class JsonObject
    {
        public static T FromJson<T>(this T _, string json) where T : class, new() => JsonConvert.DeserializeObject<T>(json);
        public static string ToJson<T>(this T _) where T : class, new() => JsonConvert.SerializeObject(_);
    }
}
