using Newtonsoft.Json;

namespace NQuadro.Shared.Serialization;

internal sealed class Serialization : ISerialization
{
    public T? Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);

    public string Serialize<T>(T data) => JsonConvert.SerializeObject(data);
}
