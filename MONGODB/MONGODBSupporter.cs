using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public class MONGODBSupporter
    {
        public MongoClient MongoClient { get; internal set; }
        public MONGODBSupporter(MongoClient mongoClient)
        {
            MongoClient = mongoClient;
            mongoClient.GetDatabase("");
        }
    }
}
