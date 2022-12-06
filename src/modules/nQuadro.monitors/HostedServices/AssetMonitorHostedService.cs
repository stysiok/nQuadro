using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using NQuadro.Monitors.Channels;

namespace NQuadro.Monitors.HostedServices;

internal sealed class AssetMonitorHostedService : BackgroundService
{
    private readonly IAssetMonitorService _assetMonitorService;
    private readonly AssetMonitorChannel _channel;
    private readonly ConcurrentDictionary<string, (IAssetMonitorService, bool)> _monitors = new ConcurrentDictionary<string, (IAssetMonitorService, bool)>();

    public AssetMonitorHostedService(IAssetMonitorService assetMonitorService, AssetMonitorChannel channel)
    {
        _assetMonitorService = assetMonitorService;
        _channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _channel.Broker.Reader.ReadAllAsync(stoppingToken))
        {
            var success = _monitors.TryGetValue(message.AssetName, out var monitor);
            if (success)
            {
                if (message.StartMonitoring)
                    _ = monitor.Item1.StartAsync(message.AssetName);
                else
                    _ = monitor.Item1.StopAsync();

                _monitors.Remove(message.AssetName, out var _);
                _monitors.TryAdd(message.AssetName, new(monitor.Item1, message.StartMonitoring));
            }
            else if (message.StartMonitoring)
            {
                var _ = _assetMonitorService.StartAsync(message.AssetName);
                _monitors.TryAdd(message.AssetName, new(_assetMonitorService, true));
            }
        }
    }
}
