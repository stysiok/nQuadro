using Microsoft.Extensions.Logging;
using NQuadro.Monitors.Feeds;
using NQuadro.Monitors.Models;
using NQuadro.Monitors.Models.Events;
using NQuadro.Monitors.Storages;
using NQuadro.Shared.Messaging;

namespace NQuadro.Monitors.Services;

internal sealed class AssetMonitorService : IAssetMonitorService
{
    private readonly IMessagePublisher _publisher;
    private readonly IAssetMonitorsStorage _assetMonitorsStorage;
    private readonly IAssetPricingFeed _assetPricingFeed;
    private readonly ILogger<IAssetMonitorService> _logger;
    private bool _running = false;

    public AssetMonitorService(IMessagePublisher publisher, IAssetMonitorsStorage assetMonitorsStorage, IAssetPricingFeed assetPricingFeed, ILogger<IAssetMonitorService> logger)
    {
        _publisher = publisher;
        _assetMonitorsStorage = assetMonitorsStorage;
        _assetPricingFeed = assetPricingFeed;
        _logger = logger;
    }

    public async Task StartAsync(AssetMonitor assetMonitor)
    {
        _running = true;

        while (_running)
        {
            var shouldBeMonitoring = assetMonitor.End > DateTime.Now;
            if (!shouldBeMonitoring)
            {
                _logger.LogWarning("Asset is no longer monitored because end date is in the past {date} < {now}", assetMonitor.End, DateTime.Now);
                await StopAsync(assetMonitor);
                break;
            }

            _logger.LogInformation("Checking how much value of {name} has changed", assetMonitor.AssetName);
            var assetPricing = await _assetPricingFeed.GetAssetPricingAsync(assetMonitor.AssetName);
            if (assetPricing is null)
            {
                _logger.LogError("Price not found for asset {name} in the thrid party system, turning off monitor", assetMonitor.AssetName);
                await StopAsync(assetMonitor);
                break;
            }

            var currentPrice = assetPricing.Price;
            var actualChange = Math.Round(Math.Abs((double)(1 - (assetMonitor.LatestPrice / currentPrice))), 5);

            if (actualChange > assetMonitor.Change)
            {
                _logger.LogInformation("Asset {name} changed significantly changed [{expectedChange} < {actualChange}] (Latest: {latest} <> Current: {current})",
                    assetMonitor.AssetName, assetMonitor.Change, actualChange, assetMonitor.LatestPrice, currentPrice);

                await _publisher.PublishAsync("asset_value_changed", new AssetValueChangedEvent(assetMonitor.AssetName, assetMonitor.Change, actualChange, currentPrice > assetMonitor.LatestPrice));

                assetMonitor = assetMonitor with { LatestPrice = currentPrice };
                await _assetMonitorsStorage.UpdateAssetMonitorAsync(assetMonitor);
            }
            else
            {
                _logger.LogInformation("Asset {name} didn't change significantly [{expectedChange} < {actualChange}]", assetMonitor.AssetName, assetMonitor.Change, actualChange);
            }

            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }

    public Task StopAsync(AssetMonitor assetMonitor)
    {
        _logger.LogInformation("Stopping monitoring for {name}", assetMonitor.AssetName);
        _running = false;
        return Task.CompletedTask;
    }
}
