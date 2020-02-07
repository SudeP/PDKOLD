using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public abstract class ObjectUniqe<BaseType> : ObjectDefault<BaseType>
    {
        public long UniqueId { get; set; }
    }
}
