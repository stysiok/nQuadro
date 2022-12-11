using NQuadro.Notifications.Models;
using NQuadro.Shared.Storage;

namespace NQuadro.Notifications.Storages;

internal sealed class AssetNotificationsStorage : IAssetNotificationsStorage
{
    private readonly IStorage _storage;
    private const string NOTIFICATIONS_KEY_PREFIX = "not-";

    public AssetNotificationsStorage(IStorage storage)
    {
        _storage = storage;
    }

    public async Task AddNotificationsAsync(string assetName, NotificationsSettings notifications)
    {
        await _storage.AddAsync($"{NOTIFICATIONS_KEY_PREFIX}{assetName}", notifications);
    }

    public async Task<NotificationsSettings> GetNotificationsAsync(string assetName)
    {
        var result = await _storage.GetAsync<NotificationsSettings>($"{NOTIFICATIONS_KEY_PREFIX}{assetName}");
        if (result is null) throw new Exception($"Notification settings for asset {assetName} has not been found");
        return result;
    }

    public Task DeleteNotificationsAsync(string assetName) => _storage.DeleteAsync($"{NOTIFICATIONS_KEY_PREFIX}{assetName}");
}
