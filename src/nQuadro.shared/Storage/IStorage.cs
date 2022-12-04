namespace NQuadro.Shared.Storage;

public interface IStorage
{
    Task<bool> ExistsAsync(string key);
    Task AddAsync<T>(string key, T data);
    Task<T?> GetAsync<T>(string key);
}
