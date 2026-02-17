using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Services;
using UrlShortener.Data.Shards;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("")]
    public class UrlController : ControllerBase
    {
        private readonly ShortCodeService _codeService;
        private readonly RedisService _redis;
        private readonly Shard1DbContext _shard1;
        private readonly Shard2DbContext _shard2;
        private readonly ShardRouter _router;

        public UrlController(
            Shard1DbContext shard1,
            Shard2DbContext shard2,
            ShortCodeService codeService,
            RedisService redis,
            ShardRouter router)
        {
            _shard1 = shard1;
            _shard2 = shard2;
            _codeService = codeService;
            _redis = redis;
            _router = router;
        }

        // 🔹 SHORTEN URL (WRITE → SHARD)
        [HttpPost("shorten")]
        public IActionResult ShortenUrl([FromBody] string url)
        {
            if (string.IsNullOrEmpty(url))
                return BadRequest("Invalid URL");

            var code = _codeService.GenerateCode();

            var shortUrl = new ShortUrl
            {
                Id = Guid.NewGuid(),
                ShortCode = code,
                OriginalUrl = url
            };

            // Decide shard
            var shardId = _router.GetShard(code);

            if (shardId == 0)
            {
                _shard1.ShortUrls.Add(shortUrl);
                _shard1.SaveChanges();
            }
            else
            {
                _shard2.ShortUrls.Add(shortUrl);
                _shard2.SaveChanges();
            }

            return Ok(new
            {
                shortCode = code,
                shortUrl = $"http://localhost:5116/{code}",
                shard = shardId   // optional debug info
            });
        }

        // 🔹 REDIRECT URL (READ → CACHE → SHARD)
        [HttpGet("{code}")]
        public IActionResult RedirectUrl(string code)
        {
            // 1️⃣ Check Redis cache
            var cachedUrl = _redis.GetUrl(code);

            if (!string.IsNullOrEmpty(cachedUrl))
                return Redirect(cachedUrl);

            // 2️⃣ Determine shard
            var shardId = _router.GetShard(code);

            ShortUrl? url = null;

            if (shardId == 0)
            {
                url = _shard1.ShortUrls
                    .FirstOrDefault(x => x.ShortCode == code);
            }
            else
            {
                url = _shard2.ShortUrls
                    .FirstOrDefault(x => x.ShortCode == code);
            }

            if (url == null)
                return NotFound("Short URL not found");

            // 3️⃣ Cache result
            _redis.SetUrl(code, url.OriginalUrl);

            return Redirect(url.OriginalUrl);
        }
    }
}
