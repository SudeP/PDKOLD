using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace PDK.Tool
{
    public class QueueTask<T>
    {
        private readonly ConcurrentQueue<Func<T>> queueList = new ConcurrentQueue<Func<T>>();
        public Task<T> Enqueue(Func<T> task)
        {
            queueList.Enqueue(task);

            return Task.Run(new Func<T>(() =>
            {
                lock (queueList)
                {
                    if (queueList.TryDequeue(out Func<T> result))
                        return result.Invoke();
                    else
                        return default;
                }
            }));
        }
    }
}
