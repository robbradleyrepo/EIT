namespace LionTrust.Feature.MyPreferences.Repositories
{
    using System;

    public interface IApplicationCacheRepository
    {
        void Write<T>(string key, T value);
        void Write<T>(string key, T value, TimeSpan duration);
        T Read<T>(string key);
        void Remove(string key);
    }
}
