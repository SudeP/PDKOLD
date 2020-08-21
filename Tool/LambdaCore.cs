using System;
using System.Collections.Generic;

namespace PDK.Tool
{
    public abstract class LambdaCore
    {
        protected int __lastId = -1;
        internal int LastId
        {
            get
            {
                return ++__lastId;
            }
        }
        public List<TryObject> TryList { get; internal set; }
        public void Run()
        {
            TryList.ForEach(@try =>
            {
                @try.Exception = RunAction(@try.Action);
                if (!(@try.Exception is null))
                {
                    @try.CatchList.ForEach(@catch =>
                    {
                        @catch.ParentTryException = @try.Exception;
                        @catch.Exception = RunAction(@catch.Action, @catch.ParentTryException);
                    });
                }
                @try.FinallyList.ForEach(@finally =>
                {
                    @finally.Exception = RunAction(@finally.Action);
                });
            });
        }
        private Exception RunAction(Action action)
        {
            Exception exception = null;
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return exception;
        }
        private Exception RunAction(Action<Exception> action, Exception parentException)
        {
            Exception exception = null;
            try
            {
                action?.Invoke(parentException);
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return exception;
        }
    }
}