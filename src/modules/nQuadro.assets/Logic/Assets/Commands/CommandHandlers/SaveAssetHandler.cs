using NQuadro.Assets.Models;
using NQuadro.Assets.Storages;
using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Logic.Assets.Commands.CommandHandlers;

internal sealed class SaveAssetHandler : ICommandHandler<SaveAsset>
{
    private readonly IAssetsStorage _assetsStorage;

    public SaveAssetHandler(IAssetsStorage assetsStorage)
    {
        _assetsStorage = assetsStorage;
    }

    public async Task HandleAsync(SaveAsset command)
    {
        var asset = new Asset(command.Name);
        await _assetsStorage.SaveAssetAsync(asset);
    }
}
