namespace LionTrust.Feature.MyPreferences.Repositories
{
    using System;
    using System.Web;
    using SystemCache = System.Web.Caching.Cache;

    public class ApplicationCacheRepository : IApplicationCacheRepository
    {
        public void Write<T>(string key, T value)
        {
            HttpContext.Current.Cache.Insert(key, value);
        }

        public void Write<T>(string key, T value, TimeSpan duration)
        {
            HttpContext.Current.Cache.Insert(key, value, null, SystemCache.NoAbsoluteExpiration, duration);
        }

        public T Read<T>(string key)
        {
            T value;
            try
            {
                if (HttpContext.Current.Cache[key] == null)
                {
                    value = default(T);
                }
                else
                {
                    value = (T)HttpContext.Current.Cache[key];
                }
            }
            catch
            {
                value = default(T);
            }

            return value;
        }

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
    }
}