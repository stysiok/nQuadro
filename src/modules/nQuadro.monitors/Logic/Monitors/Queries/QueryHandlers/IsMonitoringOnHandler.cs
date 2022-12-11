using NQuadro.Monitors.Storages;
using NQuadro.Shared.CQRS;

namespace NQuadro.Monitors.Logic.Monitors.Queries.QueryHandlers;

internal sealed class IsMonitoringOnHandler : IQueryHandler<IsMonitoringOn, bool>
{
    private readonly IAssetMonitorsStorage _storage;

    public IsMonitoringOnHandler(IAssetMonitorsStorage storage)
    {
        _storage = storage;
    }

    public async Task<bool> HandleAsync(IsMonitoringOn query)
    {
        var monitor = await _storage.GetAssetMonitorAsync(query.Asset);
        return monitor?.IsRunning ?? false;
    }
}
