using System;

namespace Tool
{
    public class LambdaFinally : LambdaCore
    {
        internal LambdaFinally()
        {

        }
        internal static LambdaFinally Create(Action action, int tryId) => new LambdaFinally().Finally(action, tryId);
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