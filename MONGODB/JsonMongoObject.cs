namespace PDK.DB.MONGODB
{
    public class JsonMongoObject<T>
    {
        public static T FromJson(string json) => MongoDB.Bson.Serialization.BsonSerializer.Deserialize<T>(json);
    }
    public static class JsonMongoObject
    {
        public static string ToJson<T>(this T _) where T : ObjectDefault<T> => MongoDB.Bson.BsonExtensionMethods.ToJson(_);
    }
}
