using NQuadro.Monitors.Models;

namespace NQuadro.Monitors.Storages;

internal interface IAssetMonitorsStorage
{
    Task AddAssetMonitorAsync(string assetName, double change, double currentPrice, DateTime end);
    Task<AssetMonitor?> GetAssetMonitorAsync(string assetName);
    Task UpdateAssetMonitorAsync(AssetMonitor assetMonitor);
}
