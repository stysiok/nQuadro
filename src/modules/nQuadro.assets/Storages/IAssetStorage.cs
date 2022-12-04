using NQuadro.Assets.Models;

namespace NQuadro.Assets.Storages;

internal interface IAssetsStorage
{
    Task SaveAssetAsync(Asset asset);
    Task<IEnumerable<Asset>> GetAssetsAsync();
}
