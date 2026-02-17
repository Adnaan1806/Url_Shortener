using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Data.Shards
{
    public class Shard2DbContext : DbContext
    {
        public Shard2DbContext(DbContextOptions<Shard2DbContext> options)
            : base(options) { }

        public DbSet<ShortUrl> ShortUrls { get; set; }
    }
}
