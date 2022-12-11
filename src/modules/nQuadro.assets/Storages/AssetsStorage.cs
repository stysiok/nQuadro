using NQuadro.Assets.Models;
using NQuadro.Shared.Storage;

namespace NQuadro.Assets.Storages;

internal sealed class AssetsStorage : IAssetsStorage
{
    private readonly IStorage _storage;
    private const string KEY = "assets";

    public AssetsStorage(IStorage storage)
    {
        _storage = storage;
    }

    public async Task<IEnumerable<Asset>> GetAssetsAsync() => await _storage.GetAsync<IEnumerable<Asset>>(KEY) ?? Enumerable.Empty<Asset>();

    public async Task SaveAssetAsync(Asset asset)
    {
        var assets = new List<Asset> { asset };
        if (await _storage.ExistsAsync(KEY))
        {
            var existingAssets = await _storage.GetAsync<IEnumerable<Asset>>(KEY) ?? Enumerable.Empty<Asset>();
            assets.AddRange(existingAssets);
        }

        await _storage.AddAsync(KEY, assets);
    }

    public async Task DeleteAssetAsync(string assetName)
    {
        var assets = await GetAssetsAsync();
        var newAssets = assets.Where(a => a.Name != assetName).ToList();
        await _storage.AddAsync(KEY, newAssets);
    }
}
