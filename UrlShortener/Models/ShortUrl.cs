using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class ShortUrl
    {
        [Key]
        public Guid Id { get; set; }

        public string ShortCode { get; set; }

        public string OriginalUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
