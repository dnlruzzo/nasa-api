using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Nasa.Domain.Constants;
using Nasa.Domain.Settings;
using Nasa.Infrastructure.HttpClients.NeoWs.Models;
using System.Net.Http.Json;

namespace Nasa.Infrastructure.HttpClients.NeoWs;

public class FeedHttpClient
{
    private readonly HttpClient _client;
    private readonly IMemoryCache _cache;
    private readonly NasaSettings _settings;

    public FeedHttpClient(HttpClient client, IMemoryCache cache, IOptions<NasaSettings> options)
    {
        _client = client;
        _cache = cache;
        _settings = options.Value;
    }

    public async Task<Feed?> GetAsync(int days, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        var startDate = now.ToString(DateFormatConstants.yyyy_MM_dd);
        var endDate = now.AddDays(days).ToString(DateFormatConstants.yyyy_MM_dd);

        var cacheKey = $"asteroids:feed:{startDate}:{endDate}";

        var feed = _cache.Get<Feed?>(cacheKey);
        if (feed is null)
        {
            feed = await _client.GetFromJsonAsync<Feed?>($"?start_date={startDate}&end_date={endDate}&api_key={_settings.ApiKey}", cancellationToken);
            _cache.Set(cacheKey, feed);
        }

        return feed;
    }
}
