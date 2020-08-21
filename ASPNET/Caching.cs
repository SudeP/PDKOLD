using System;
using System.Web.Caching;

namespace PDK.ASPNET
{
    public class Caching
    {
        private readonly Cache cache;
        public Caching() => cache = new Cache();
        public T Get<T>(string key) where T : class => cache.Get(key) as T;
        public void Set<T>(T obj, string key, DateTime dateTime) where T : class => cache.Insert(key, obj, null, dateTime, Cache.NoSlidingExpiration);
        public T GetOrSet<T>(Func<T> action, string key, DateTime dateTime) where T : class
        {
            if (Get<T>(key) is T orjObj && orjObj != null)
                return orjObj;
            else
                return GetOrSet<T>(action.Invoke(), key, dateTime);
        }
        public T GetOrSet<T>(T obj, string key, DateTime dateTime) where T : class
        {
            if (Get<T>(key) is T orjObj && orjObj != null)
                return orjObj;
            else
                Set(obj, key, dateTime);
            return obj;
        }
    }
}
