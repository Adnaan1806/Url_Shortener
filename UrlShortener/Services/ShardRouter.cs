namespace UrlShortener.Services
{
    public class ShardRouter
    {
        public int GetShard(string shortCode)
        {
            var hash = shortCode.GetHashCode();
            return Math.Abs(hash % 2); // 2 shards
        }
    }
}
