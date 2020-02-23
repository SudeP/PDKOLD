using System;

namespace Tool
{
    public class LambdaCatch : LambdaCore
    {
        internal LambdaCatch()
        {

        }
        internal static LambdaCatch Create(Action<Exception> action, int tryId) => new LambdaCatch().Catch(action, tryId);
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
        public LambdaFinally Finally(Action action, int tryId = -1) => LambdaFinally.Create(action, tryId);
    }
}