using System.ComponentModel.DataAnnotations;

namespace NQuadro.Assets.Models.Requests;

public sealed class SaveAssetRequest
{
    [Required]
    public string? Name { get; init; }
    public double Change { get; init; }
    [Required]
    public DateTime End { get; init; }
}
