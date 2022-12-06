using NQuadro.Monitors.Channels;
using NQuadro.Shared.CQRS;

namespace NQuadro.Monitors.Logic.Monitors.Commands.CommandHandlers;

internal sealed class StartMonitoringAssetHandler : ICommandHandler<StartMonitoringAsset>
{
    private readonly AssetMonitorChannel _channel;

    public StartMonitoringAssetHandler(AssetMonitorChannel channel)
    {
        _channel = channel;
    }

    public async Task HandleAsync(StartMonitoringAsset command)
    {
        await _channel.Broker.Writer.WriteAsync(new AssetMonitorChannelMessage(command.Name, StartMonitoring: true));
    }
}
