using NQuadro.Monitors.Models.Feeds;

namespace NQuadro.Monitors.Feeds;

internal interface IAssetPricingFeed
{
    Task<AssetPricing?> GetAssetPricingAsync(string assetName);
}
