using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public class JsonMongoObject<BaseType>
    {
        public static BaseType FromJson(string json) => MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BaseType>(json);
    }
    public static class JsonMongoObject
    {
        public static string ToJson<T>(this T _) where T : ObjectDefault<T> => MongoDB.Bson.BsonExtensionMethods.ToJson(_);
    }
}
