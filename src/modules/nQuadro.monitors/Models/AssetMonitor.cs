namespace NQuadro.Monitors.Models;

internal sealed record AssetMonitor(string AssetName, double Change, double LatestPrice, DateTime End, bool IsRunning);
