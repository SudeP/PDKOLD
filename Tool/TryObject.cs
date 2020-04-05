using System;
using System.Collections.Generic;

namespace PDK.Tool
{
    public class TryObject : ActionableObject
    {
        public Action Action { get; internal set; }
        public List<CatchObject> CatchList { get; internal set; }
        public List<FinallyObject> FinallyList { get; internal set; }
        public TryObject()
        {
            CatchList = new List<CatchObject>();
            FinallyList = new List<FinallyObject>();
        }
    }
}