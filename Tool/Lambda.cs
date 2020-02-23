using System;
using System.Collections.Generic;

namespace Tool
{
    public class Lambda : LambdaCore
    {
        internal Lambda()
        {

        }
        public static Lambda Create()
        {
            __lastId = -1;
            TryList = new List<TryObject>();
            GC.Collect();
            return new Lambda();
        }
        public Lambda Try(Action action)
        {
            TryList.Add(new TryObject()
            {
                Id = LastId,
                Action = action
            });
            return this;
        }
        public LambdaCatch Catch(Action<Exception> action, int tryId = -1) => LambdaCatch.Create(action, tryId);
        public LambdaFinally Finally(Action action, int tryId = -1) => LambdaFinally.Create(action, tryId);
    }
}