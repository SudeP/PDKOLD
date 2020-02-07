using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public abstract class ObjectDefault<BaseType> : JsonMongoObject<BaseType>
    {
#pragma warning disable IDE1006 // Naming Styles
        public object _id { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
