using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Api.Helpers;
using UrlShortenerApi.Data;
using UrlShortenerApi.Extensions;
using UrlShortenerApi.Models;
using UrlShortenerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<UrlShorteningService>();
builder.Services.AddScoped<CacheService>();
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "s-url-";
});
builder.Services.AddCors();

var app = builder.Build();

DatabaseMigrator.ApplyMigration(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapGet("/api/{code}", async Task<IResult> (
    string code,
    ApplicationDbContext dbContext,
    CacheService cache) =>
{
    var cachedUrl = await cache.GetAsync<ShortenedUrl>(code);
    if (cachedUrl.Success) return Results.Redirect(cachedUrl.Value.BaseUrl);

    var shortenedUrl = await dbContext.ShortenedUrls.FirstOrDefaultAsync(u => u.Code == code);
    if (shortenedUrl == null) return Results.NotFound();

    await cache.SetAsync(shortenedUrl.Code, shortenedUrl, TimeSpan.FromMinutes(15));

    return Results.Redirect(shortenedUrl.BaseUrl);
});

app.MapPost("/api/", async (
    [FromBody] ShortenedUrlRequest request,
    UrlShorteningService urlShorteningService,
    ApplicationDbContext dbContext,
    CacheService cacheService) =>
{
    if (!UrlValidator.IsValid(request.Url)) return Results.BadRequest("Invalid URL");
    request.Url = request.Url.TrimEnd('/');

    var shortenedUrl = await dbContext.ShortenedUrls.FirstOrDefaultAsync(u => u.BaseUrl == request.Url);
    if (shortenedUrl is null)
    {
        shortenedUrl = await urlShorteningService.GenerateShortenedUrlAsync(request.Url);

        dbContext.ShortenedUrls.Add(shortenedUrl);
        await dbContext.SaveChangesAsync();
    }

    await cacheService.SetAsync(shortenedUrl.Code, shortenedUrl, TimeSpan.FromMinutes(15));

    return Results.Ok(shortenedUrl.ShortUrl);
});

app.Run();