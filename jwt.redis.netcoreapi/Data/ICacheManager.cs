using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Data
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        void Set(string key, object value, TimeSpan timeSpan);
        void Remove(string key);
        bool Any(string key);
    }
}
