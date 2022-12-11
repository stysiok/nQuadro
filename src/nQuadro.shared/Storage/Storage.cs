using NQuadro.Shared.Serialization;
using StackExchange.Redis;

namespace NQuadro.Shared.Storage;

internal sealed class Storage : IStorage
{
    private readonly IDatabase _db;
    private readonly ISerialization _serialization;

    public Storage(IConnectionMultiplexer connectionMultiplexer, ISerialization serialization)
    {
        _db = connectionMultiplexer.GetDatabase();
        _serialization = serialization;
    }

    public async Task AddAsync<T>(string key, T data)
    {
        var jsonData = _serialization.Serialize(data);
        await _db.StringSetAsync(key, jsonData);
    }

    public Task DeleteAsync(string key) => _db.KeyDeleteAsync(key);


    public Task<bool> ExistsAsync(string key) => _db.KeyExistsAsync(key);

    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await _db.StringGetAsync(key);
        if (string.IsNullOrWhiteSpace(data)) return default;

        return _serialization.Deserialize<T>(data.ToString());
    }

}
