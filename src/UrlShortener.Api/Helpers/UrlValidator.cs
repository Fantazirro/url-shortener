namespace UrlShortener.Api.Helpers
{
    public static class UrlValidator
    {
        public static bool IsValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}