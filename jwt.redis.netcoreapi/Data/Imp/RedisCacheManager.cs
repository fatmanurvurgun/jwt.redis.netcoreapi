using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Data.Imp
{
    public class RedisCacheManager : ICacheManager
    {
        public bool Any(string key)
        {
            return RedisStore.RedisCache.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (Any(key))
            {
                string jsonData = RedisStore.RedisCache.StringGet(key);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default;
        }

        public void Remove(string key)
        {
            RedisStore.RedisCache.KeyDelete(key);
        }

        public void Set(string key, object value, TimeSpan timeSpan)
        {
            var expiresIn = timeSpan;
            string jsonData = JsonConvert.SerializeObject(value);
            RedisStore.RedisCache.StringSet(key, jsonData, expiresIn);
        }
    }
}
