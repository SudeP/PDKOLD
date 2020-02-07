using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public static class MemberValueMethods
    {
        public static DateTime UnixTimeToDateTime(this long unixTimeStamp) => new BsonDateTime(unixTimeStamp).ToUniversalTime();
    }
}
