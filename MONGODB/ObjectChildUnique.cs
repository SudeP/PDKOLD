using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.DB.MONGODB
{
    public abstract class ObjectChildUnique<BaseType> : ObjectUniqe<BaseType>
    {
        public long ParentId { get; set; }
    }
}
