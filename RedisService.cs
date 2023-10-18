using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisTest
{
    public sealed class RedisService
    {
        private static ConcurrentDictionary<string, Lazy<ConnectionMultiplexer>> _connectionPool = new();
        public IDatabase Connect(string setting = "localhost")
        {
            var connMultiplexer = _connectionPool.GetOrAdd(setting,
                new Lazy<ConnectionMultiplexer>(() =>
                {
                    return ConnectionMultiplexer.Connect(setting);
                }));

            return connMultiplexer.Value.GetDatabase();
        }

        public IDatabase GetDatabase(string setting = "localhost")
        {
            if (_connectionPool.TryGetValue(setting, out var connectionMultiplexer))
            {
                return connectionMultiplexer.Value.GetDatabase();
            }

            return default;
        }

        public bool IsConnected(string setting = "localhost")
        {
            if (_connectionPool.TryGetValue(setting, out var connectionMultiplexer))
            {
                return connectionMultiplexer.Value.IsConnected;
            }

            return false;
        }

        //private static string _settingOption;
        //private static Lazy<RedisService> _lazy = new Lazy<RedisService>(() =>
        //{
        //    if (String.IsNullOrEmpty(_settingOption)) throw new InvalidOperationException("Please call Init() first.");
        //    return new RedisService();
        //});
        //private ConnectionMultiplexer ConnectionMultiplexer;
        //public IDatabase Database => this.ConnectionMultiplexer.GetDatabase();

        //public static RedisService Instance
        //{
        //    get
        //    {
        //        return _lazy.Value;
        //    }
        //}

        //private RedisService()
        //{
        //    ConnectionMultiplexer = ConnectionMultiplexer.Connect(_settingOption);
        //}

        //public static void Init(string settingOption)
        //{
        //    _settingOption = settingOption;
        //}
    }
}
