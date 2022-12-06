using Microsoft.Extensions.Hosting;
using NQuadro.Monitors.Models;
using NQuadro.Shared.Messaging;

namespace NQuadro.Monitors.HostedServices
{
    internal sealed class AssetAddedBackgroundService : BackgroundService
    {
        private readonly IMessageReceiver _receiver;

        public AssetAddedBackgroundService(IMessageReceiver receiver)
        {
            _receiver = receiver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _receiver.ReceiverAsync<Asset>("new_asset_added", (data) =>
            {
                Console.WriteLine($"--- new asset {data.Name} Monitors---");
            });
        }
    }
}
