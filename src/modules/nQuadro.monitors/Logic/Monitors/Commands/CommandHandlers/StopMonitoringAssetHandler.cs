using NQuadro.Monitors.Channels;
using NQuadro.Shared.CQRS;

namespace NQuadro.Monitors.Logic.Monitors.Commands.CommandHandlers;

internal sealed class StopMonitoringAssetHandler : ICommandHandler<StopMonitoringAsset>
{
    private readonly AssetMonitorChannel _channel;

    public StopMonitoringAssetHandler(AssetMonitorChannel channel)
    {
        _channel = channel;
    }

    public async Task HandleAsync(StopMonitoringAsset command)
    {
        await _channel.Broker.Writer.WriteAsync(new AssetMonitorChannelMessage(command.Name, StartMonitoring: false));
    }
}
