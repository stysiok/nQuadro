namespace NQuadro.Monitors.Services;

internal interface IAssetMonitorService
{
    Task StartAsync(string assetName);
    Task StopAsync(string assetName);
}
