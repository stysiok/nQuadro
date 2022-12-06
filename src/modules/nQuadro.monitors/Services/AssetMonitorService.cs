using Microsoft.Extensions.Logging;
using NQuadro.Monitors.Models.Events;
using NQuadro.Shared.Messaging;

namespace NQuadro.Monitors.Services;

internal sealed class AssetMonitorService : IAssetMonitorService
{
    private readonly IMessagePublisher _publisher;
    private readonly ILogger<IAssetMonitorService> _logger;
    private readonly Random _random;
    private bool _running = false;

    public AssetMonitorService(IMessagePublisher publisher, ILogger<IAssetMonitorService> logger)
    {
        _publisher = publisher;
        _logger = logger;
        _random = new Random();
    }

    public async Task StartAsync(string assetName)
    {
        _running = true;

        while (_running)
        {
            _logger.LogInformation("Checking how much value of {name} has changed", assetName);

            if (_random.NextDouble() > 0.80)
            {
                _logger.LogInformation("Asset {name} changed significantly changed, sending message and updating asset {name}", assetName, assetName);
                await _publisher.PublishAsync("asset_value_changed", new AssetValueChangedEvent(assetName, _random.Next(1, 10)));
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    public Task StopAsync(string assetName)
    {
        _logger.LogInformation("Stopping monitoring for {name}", assetName);
        _running = false;
        return Task.CompletedTask;
    }
}
