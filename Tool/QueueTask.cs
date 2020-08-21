using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PDK.Tool
{
    public class QueueTask
    {
        private readonly ConcurrentQueue<Func<object>> queueList = new ConcurrentQueue<Func<object>>();
        public Task Enqueue(Action task)
        {
            return Enqueue(() =>
            {
                task.Invoke();
                return new object();
            });
        }
        public Task<T> Enqueue<T>(Func<T> task) where T : class
        {
            queueList.Enqueue(task);

            return Task.Run(new Func<T>(() =>
            {
                lock (queueList)
                {
                    if (queueList.TryDequeue(out Func<object> result))
                    {
                        var r = result.Invoke();
                        return r as T;
                    }
                    else
                        return default;
                }
            }));
        }
    }
}
