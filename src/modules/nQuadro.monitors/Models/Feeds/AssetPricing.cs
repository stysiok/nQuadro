using System.Text.Json.Serialization;

namespace NQuadro.Monitors.Models.Feeds;

internal sealed record AssetPricing
{
    [JsonPropertyName("ticker")]
    public string AssetName { get; init; } = "";

    [JsonPropertyName("price")]
    public double Price { get; init; }
}
