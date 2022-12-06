using System.ComponentModel.DataAnnotations;

namespace NQuadro.Monitors.Models.Requests;

public sealed class StartMonitoringAssetRequest
{
    [Required]
    public string? Name { get; init; }
}
