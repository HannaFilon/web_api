namespace Shop.Business.IServices
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void AddToCache<T>(T o, string key);
        void RemoveFromCache(string key);
    }
}