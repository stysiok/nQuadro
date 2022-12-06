using System.ComponentModel.DataAnnotations;

namespace NQuadro.Monitors.Models.Requests;

public sealed class StopMonitoringAssetRequest
{
    [Required]
    public string? Name { get; init; }
}
