using System;

namespace PDK.Tool
{
    public class CatchObject : ActionableObject
    {
        public Action<Exception> Action { get; internal set; }
        public Exception ParentTryException { get; internal set; }
    }
}