using StackExchange.Redis;
using System;

namespace RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! Redis Test");

            var redis = new RedisService();
            // 建立 Redis 連線
            var redisConfiguration = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" }, // Redis 伺服器位置和端口號
                AbortOnConnectFail = false // 在連接失敗時不中止重試
            };
            var db = redis.Connect(redisConfiguration.ToString());
            Console.WriteLine($"redis is connect {redis.IsConnected(redisConfiguration.ToString())}");

            // 寫入鍵值對
            string key = "example_key";
            string value = "Hello, Redis! ";
            var model = new Model
            {
                Name = "小名",
                Age = 30
            };
            db.Set(key, model);

            // 讀取鍵值對
            var retrievedValue = db.Get<Model>(key);
            Console.WriteLine($"Key: {key}, Value: {retrievedValue.Name + retrievedValue.Age}");


            //var mux = ConnectionMultiplexer.Connect("localhost:6379, abortConnect=false");
            //var db = mux.GetDatabase();
            //Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: Redis connection is established.");

            //var key = "MyCache:foo";
            //var value = "shared cache data";

            //db.StringSet(key, value);
            //Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: StringSet: key:{key}, value:{value}");
            //Console.WriteLine("Press any key to continue...");

            //Console.WriteLine($"get MyCache:foo={db.StringGet(key)}"); 
            //Console.Read();
        }

        public class Model
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
