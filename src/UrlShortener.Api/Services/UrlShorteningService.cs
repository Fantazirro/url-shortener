using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Configurations;
using UrlShortenerApi.Data;
using UrlShortenerApi.Models;

namespace UrlShortenerApi.Services
{
    public class UrlShorteningService(ApplicationDbContext dbContext)
    {
        public async Task<ShortenedUrl> GenerateShortenedUrlAsync(string url)
        {         
            var domain = Environment.GetEnvironmentVariable("Domain");
            var code = await GenerateUniqueCode();

            var shortenedUrl = new ShortenedUrl()
            {
                Code = code,
                BaseUrl = url,
                ShortUrl = $"{domain}/{code}",
                CreatedOnUtc = DateTime.UtcNow
            };

            return shortenedUrl;
        }

        private async Task<string> GenerateUniqueCode()
        {
            while (true)
            {
                var code = GenerateCode();
                var codeExists = await dbContext.ShortenedUrls.AnyAsync(u => u.Code == code);
                if (!codeExists) return code;
            }
        }

        private string GenerateCode()
        {
            var code = new char[UrlShortenerConfiguration.CodeLength];
            
            for (int i = 0; i < code.Length; i++)
            {
                var currentCharIndex = new Random().Next(0, UrlShortenerConfiguration.AlphabetSize);
                code[i] = UrlShortenerConfiguration.Alphabet[currentCharIndex];
            }

            return new string(code);
        }
    }
}