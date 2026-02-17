using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("")]
    public class UrlController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ShortCodeService _codeService;

        public UrlController(
            AppDbContext context,
            ShortCodeService codeService)
        {
            _context = context;
            _codeService = codeService;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] string url)
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

            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                shortCode = code,
                shortUrl = $"http://localhost:5116/{code}"
            });
        }

        [HttpGet("{code}")]
        public IActionResult RedirectUrl(string code)
        {
           var url = _context.ShortUrls
              .FirstOrDefault(x => x.ShortCode == code);

           if (url == null)
              return NotFound("Short URL not found");

          return Redirect(url.OriginalUrl);
        }

    }
}
