using StackExchange.Redis;
using System.Text.Json;

namespace BankOfMikaila.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cacheDb;
        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb = redis.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                return JsonSerializer.Deserialize<T>(value);
            }

            return default; //if empty, return null
        }

        public object RemoveData(string key)
        {
            var _exist = _cacheDb.KeyExists(key);

            if (_exist)
            {
                return _cacheDb.KeyDelete(key);
            }

            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);

            return isSet;
        }

        public bool AddData<T>(string key, T value)
        {
            var isAdded = _cacheDb.SetAdd(key, JsonSerializer.Serialize(value));

            return isAdded;
        }

        public object Invalidate(string key)
        {
            return _cacheDb.StringGetSetExpiry(key,DateTime.Now);
        }

        
    }
}
