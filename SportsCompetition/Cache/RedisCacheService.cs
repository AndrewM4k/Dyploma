using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json;

namespace WebApplication1.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _database;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public void DeleteValue<T>(string key)
        {
            var json = _database.StringGet(key);
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            _database.KeyDelete(key);
        }

        public void DeleteValue(string key)
        {
            var json = _database.StringGet(key);
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            _database.KeyDelete(key);
        }

        public void DeleteValue<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void DeleteValue(string key, string value)
        {
            throw new NotImplementedException();
        }

        public string GetString(string key)
        {
            return _database.StringGet(key);
        }

        public T GetValue<T>(string key)
        {
            var json = _database.StringGet(key);
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void SetValue<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            SetValue(key, json);
        }

        public void SetValue(string key, string value)
        {
            _database.StringSet(key, value, expiry: TimeSpan.FromHours(2));
        }

        public T UpdateValue<T>(string key)
        {
            DeleteValue(key);
            return GetValue<T>( key);
        }

        public string UpdateValue(string key)
        {
            DeleteValue(key);
            return GetString(key);
        }
    }
}
