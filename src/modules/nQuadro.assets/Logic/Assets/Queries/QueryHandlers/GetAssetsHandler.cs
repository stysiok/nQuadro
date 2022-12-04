using NQuadro.Assets.Models.DTOs;
using NQuadro.Assets.Storages;
using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Logic.Assets.Queries.QueryHandlers;

internal sealed class GetAssetsHandler : IQueryHandler<GetAssets, IEnumerable<AssetDTO>>
{
    private readonly IAssetsStorage _assetsStorage;

    public GetAssetsHandler(IAssetsStorage assetsStorage)
    {
        _assetsStorage = assetsStorage;
    }

    public async Task<IEnumerable<AssetDTO>> HandleAsync(GetAssets query)
    {
        var assets = await _assetsStorage.GetAssetsAsync();

        return assets.Select(a => new AssetDTO(a.Name));
    }
}
