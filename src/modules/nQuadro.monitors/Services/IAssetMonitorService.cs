using NQuadro.Monitors.Models;

namespace NQuadro.Monitors.Services;

internal interface IAssetMonitorService
{
    Task StartAsync(AssetMonitor assetMonitor);
    Task StopAsync(AssetMonitor assetMonitor);
}
