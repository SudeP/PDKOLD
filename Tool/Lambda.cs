using System;
using System.Collections.Generic;

namespace PDK.Tool
{
    public class Lambda : LambdaCore
    {
        private Lambda() => TryList = new List<TryObject>();
        public static Lambda Create() => new Lambda();
        public Lambda Try(Action action)
        {
            TryList.Add(new TryObject()
            {
                Id = LastId,
                Action = action
            });
            return this;
        }
        public LambdaCatch Catch(Action<Exception> action, int tryId = -1) => LambdaCatch.Create(TryList, action, tryId);
        public LambdaFinally Finally(Action action, int tryId = -1) => LambdaFinally.Create(TryList, action, tryId);
    }
}