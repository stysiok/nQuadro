using System.Collections.Concurrent;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NQuadro.Monitors.Channels;
using NQuadro.Monitors.Services;
using NQuadro.Monitors.Storages;

namespace NQuadro.Monitors.HostedServices;

internal sealed class AssetMonitorManager : BackgroundService
{
    private readonly IAssetMonitorService _assetMonitorService;
    private readonly AssetMonitorChannel _channel;
    private readonly ILogger<AssetMonitorManager> _logger;
    private readonly ConcurrentDictionary<string, (IAssetMonitorService, bool)> _monitors = new();
    private readonly IAssetMonitorsStorage _assetMonitorsStorage;

    public AssetMonitorManager(IAssetMonitorService assetMonitorService, IAssetMonitorsStorage assetMonitorsStorage, AssetMonitorChannel channel, ILogger<AssetMonitorManager> logger)
    {
        _assetMonitorService = assetMonitorService;
        _channel = channel;
        _logger = logger;
        _assetMonitorsStorage = assetMonitorsStorage;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _channel.Broker.Reader.ReadAllAsync(stoppingToken))
        {
            var assetMonitor = await _assetMonitorsStorage.GetAssetMonitorAsync(message.AssetName);
            if (assetMonitor is null)
            {
                _logger.LogError("Asset monitor with key {name} does not exists", message.AssetName);
                return;
            }

            var success = _monitors.TryGetValue(assetMonitor.AssetName, out var monitor);
            if (success)
            {
                if (message.StartMonitoring)
                    _ = monitor.Item1.StartAsync(assetMonitor);
                else
                    _ = monitor.Item1.StopAsync(assetMonitor);

                _monitors.Remove(assetMonitor.AssetName, out var _);
                _monitors.TryAdd(assetMonitor.AssetName, new(monitor.Item1, message.StartMonitoring));
            }
            else if (message.StartMonitoring)
            {
                var _ = _assetMonitorService.StartAsync(assetMonitor);
                _monitors.TryAdd(assetMonitor.AssetName, new(_assetMonitorService, true));
            }
        }
    }
}
