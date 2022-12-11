using NQuadro.Monitors.Models;

namespace NQuadro.Monitors.Storages;

internal interface IAssetMonitorsStorage
{
    Task AddAssetMonitorAsync(string assetName, double change, double currentPrice, DateTime end, bool isRunning);
    Task<AssetMonitor?> GetAssetMonitorAsync(string assetName);
    Task UpdateAssetMonitorAsync(AssetMonitor assetMonitor);
    Task DeleteAssetMonitorAsync(string assetName);
}
