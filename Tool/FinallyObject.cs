using System;
using System.Collections.Generic;
using System.Text;

namespace PDK.Tool
{
    public class FinallyObject : ActionableObject
    {
        public Action Action { get; internal set; }
    }
}
