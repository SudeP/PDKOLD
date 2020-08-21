using System;
using System.Collections.Generic;

namespace PDK.Tool
{
    public class LambdaFinally : LambdaCore
    {
        internal LambdaFinally(List<TryObject> list)
        {
            TryList = list;
        }
        public static LambdaFinally Create(List<TryObject> list, Action action, int tryId) => new LambdaFinally(list).Finally(action, tryId);
        public LambdaFinally Finally(Action action, int tryId = -1)
        {
            if (tryId == -1)
            {
                TryList.ForEach(@try =>
                {
                    @try.FinallyList.Add(new FinallyObject()
                    {
                        Id = LastId,
                        Action = action
                    });
                });
            }
            else
            {
                TryList[tryId].FinallyList.Add(new FinallyObject()
                {
                    Id = LastId,
                    Action = action
                });
            }
            return this;
        }
    }
}