using StackExchange.Redis;

namespace UrlShortener.Services
{
    public class RedisService
    {
        private readonly IDatabase _db;

        public RedisService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _db = redis.GetDatabase();
        }

        public string? GetUrl(string code)
        {
            return _db.StringGet(code);
        }

        public void SetUrl(string code, string url)
        {
            _db.StringSet(code, url, TimeSpan.FromHours(1));
        }
    }
}
