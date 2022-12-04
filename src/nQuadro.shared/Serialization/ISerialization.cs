namespace NQuadro.Shared.Serialization;

public interface ISerialization
{
    T? Deserialize<T>(string data);
    string Serialize<T>(T data);
}
