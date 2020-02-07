using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public abstract class ObjectChild<BaseType> : ObjectDefault<BaseType>
    {
        public long ParentId { get; set; }
    }
}
