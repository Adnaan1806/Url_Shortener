using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data.Shards
{
    public class Shard1DbContext : DbContext
    {
        public Shard1DbContext(DbContextOptions<Shard1DbContext> options)
            : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
