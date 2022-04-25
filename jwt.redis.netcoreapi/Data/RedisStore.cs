using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Data
{
    public static class RedisStore
    {

        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;
        private static string _configurationOptions;

        static RedisStore()
        {
            _configurationOptions = GetConnectionString();
            LazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_configurationOptions));
        }

        public static ConnectionMultiplexer Connection => LazyConnection.Value;
        public static IDatabase RedisCache => Connection.GetDatabase();

        private static string GetConnectionString()
        {

            string host = Startup.Configuration.GetSection("RedisConfiguration:Host").Value;
            string port = Startup.Configuration.GetSection("RedisConfiguration:Port").Value;
            string connectionString = $"{host}:{port},abortConnect=false";

            return connectionString;
        }

        public static void FlushDatabase()
        {
            Connection.GetServer(GetConnectionString()).FlushDatabase();
        }
    }
}