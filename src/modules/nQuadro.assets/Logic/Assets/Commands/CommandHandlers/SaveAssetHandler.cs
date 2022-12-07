using Microsoft.Extensions.Logging;
using NQuadro.Assets.Models;
using NQuadro.Assets.Models.Events;
using NQuadro.Assets.Storages;
using NQuadro.Shared.CQRS;
using NQuadro.Shared.Messaging;

namespace NQuadro.Assets.Logic.Assets.Commands.CommandHandlers;

internal sealed class SaveAssetHandler : ICommandHandler<SaveAsset>
{
    private readonly IAssetsStorage _assetsStorage;
    private readonly IMessagePublisher _publisher;
    private readonly ILogger<SaveAssetHandler> _logger;

    public SaveAssetHandler(IAssetsStorage assetsStorage, IMessagePublisher publisher, ILogger<SaveAssetHandler> logger)
    {
        _assetsStorage = assetsStorage;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task HandleAsync(SaveAsset command)
    {
        var asset = new Asset(command.Name, command.Change, command.End);
        await _assetsStorage.SaveAssetAsync(asset);

        _logger.LogInformation("Saved asset {name} to storage", command.Name);
        await _publisher.PublishAsync("asset_added", new AssetAdded(asset.Name, asset.Change, asset.End));
    }
}
