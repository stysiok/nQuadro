namespace NQuadro.Monitors.HostedServices;

internal sealed class AssetMonitorService : IAssetMonitorService
{
    private bool _running = false;

    public async Task StartAsync(string assetName)
    {
        _running = true;

        while (_running)
        {
            Console.WriteLine($"running {DateTime.Now} {assetName}");
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    public Task StopAsync()
    {
        _running = false;
        return Task.CompletedTask;
    }
}
