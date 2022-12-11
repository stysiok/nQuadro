using Microsoft.Extensions.Logging;
using NQuadro.Monitors.Models;
using NQuadro.Shared.Storage;

namespace NQuadro.Monitors.Storages;

internal sealed class AssetMonitorsStorage : IAssetMonitorsStorage
{
    private readonly IStorage _storage;
    private readonly ILogger<AssetMonitorsStorage> _logger;

    public AssetMonitorsStorage(IStorage storage, ILogger<AssetMonitorsStorage> logger)
    {
        _storage = storage;
        _logger = logger;
    }

    public async Task AddAssetMonitorAsync(string assetName, double change, double currentPrice, DateTime end, bool isRunning)
    {
        await _storage.AddAsync<AssetMonitor>(assetName, new(assetName, change, currentPrice, end, isRunning));
        _logger.LogInformation("Added asset {name} to monitors storage", assetName);
    }

    public async Task<AssetMonitor?> GetAssetMonitorAsync(string assetName)
    {
        _logger.LogInformation("Getting asset {name} from storage", assetName);
        var assetMonitor = await _storage.GetAsync<AssetMonitor>(assetName);

        if (assetMonitor is null)
        {
            _logger.LogError("Asset {name} not found in the storage", assetName);
            return default;
        }

        _logger.LogInformation("Found asset {name} in the storage", assetMonitor.AssetName);
        return assetMonitor;
    }

    public async Task UpdateAssetMonitorAsync(AssetMonitor assetMonitor)
    {
        await _storage.AddAsync(assetMonitor.AssetName, assetMonitor);
        _logger.LogInformation("Updated asset {name} to monitors storage", assetMonitor.AssetName);
    }
}
