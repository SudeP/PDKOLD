using System;
using System.Collections.Generic;

namespace PDK.Tool
{
    public class LambdaCatch : LambdaCore
    {
        private LambdaCatch(List<TryObject> list)
        {
            TryList = list;
        }
        public static LambdaCatch Create(List<TryObject> list, Action<Exception> action, int tryId) => new LambdaCatch(list).Catch(action, tryId);
        public LambdaCatch Catch(Action<Exception> action, int tryId = -1)
        {
            if (tryId == -1)
            {
                TryList.ForEach(@try =>
                {
                    @try.CatchList.Add(new CatchObject()
                    {
                        Id = LastId,
                        Action = action
                    });
                });
            }
            else
            {
                TryList[tryId].CatchList.Add(new CatchObject()
                {
                    Id = LastId,
                    Action = action
                });
            }
            return this;
        }
        public LambdaFinally Finally(Action action, int tryId = -1) => LambdaFinally.Create(TryList, action, tryId);
    }
}