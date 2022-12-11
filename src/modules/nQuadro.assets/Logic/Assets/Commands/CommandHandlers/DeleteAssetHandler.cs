using Microsoft.Extensions.Logging;
using NQuadro.Assets.Models.Events;
using NQuadro.Assets.Storages;
using NQuadro.Shared.CQRS;
using NQuadro.Shared.Messaging;

namespace NQuadro.Assets.Logic.Assets.Commands.CommandHandlers;

internal sealed class DeleteAssetHandler : ICommandHandler<DeleteAsset>
{
    private readonly IAssetsStorage _assetsStorage;
    private readonly IMessagePublisher _publisher;
    private readonly ILogger<DeleteAssetHandler> _logger;

    public DeleteAssetHandler(IAssetsStorage assetsStorage, IMessagePublisher publisher, ILogger<DeleteAssetHandler> logger)
    {
        _assetsStorage = assetsStorage;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task HandleAsync(DeleteAsset command)
    {
        await _assetsStorage.DeleteAssetAsync(command.Name);
        _logger.LogInformation("Deleted asset {name} from storage", command.Name);
        await _publisher.PublishAsync("asset_deleted", new AssetDeleted(command.Name));
    }
}
