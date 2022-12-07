namespace NQuadro.Monitors.Models.Events;

internal sealed record AssetValueChangedEvent(string Name, double ExpectedChange, double ActualChange, bool PriceIncreased);
