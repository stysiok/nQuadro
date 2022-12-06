using System.Threading.Channels;

namespace NQuadro.Monitors.Channels;

internal sealed class AssetMonitorChannel
{
    public Channel<AssetMonitorChannelMessage> Broker = Channel.CreateUnbounded<AssetMonitorChannelMessage>();
}
