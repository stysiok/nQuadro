using NQuadro.Assets.Models;
using NQuadro.Assets.Storages;
using NQuadro.Shared.CQRS;
using NQuadro.Shared.Messaging;

namespace NQuadro.Assets.Logic.Assets.Commands.CommandHandlers;

internal sealed class SaveAssetHandler : ICommandHandler<SaveAsset>
{
    private readonly IAssetsStorage _assetsStorage;
    private readonly IMessagePublisher _publisher;

    public SaveAssetHandler(IAssetsStorage assetsStorage, IMessagePublisher publisher)
    {
        _assetsStorage = assetsStorage;
        _publisher = publisher;
    }

    public async Task HandleAsync(SaveAsset command)
    {
        var asset = new Asset(command.Name, command.Change, command.Start, command.End);
        await _assetsStorage.SaveAssetAsync(asset);
        await _publisher.PublishAsync("new_asset_added", asset);
    }
}
