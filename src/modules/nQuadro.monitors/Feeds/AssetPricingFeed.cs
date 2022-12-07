using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NQuadro.Monitors.Models.Configurations;
using NQuadro.Monitors.Models.Feeds;

namespace NQuadro.Monitors.Feeds;

internal sealed class AssetPricingFeed : IAssetPricingFeed
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly DarqubeConfiguration _darqubeConfiguration;

    public AssetPricingFeed(IHttpClientFactory httpClientFactory, IOptions<DarqubeConfiguration> darqubeConfiguration)
    {
        _httpClientFactory = httpClientFactory;
        _darqubeConfiguration = darqubeConfiguration.Value;
    }

    public async Task<AssetPricing?> GetAssetPricingAsync(string assetName)
    {
        using var client = _httpClientFactory.CreateClient();
        var url = $"{_darqubeConfiguration.Url}/data-api/market_data/quote/{assetName}?token={_darqubeConfiguration.Token}";
        var result = await client.GetFromJsonAsync<AssetPricing>(url);
        return result;
    }
}
