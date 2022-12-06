namespace NQuadro.Monitors.HostedServices;

internal interface IAssetMonitorService
{
    Task StartAsync(string assetName);
    Task StopAsync();
}
