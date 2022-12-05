using System.ComponentModel.DataAnnotations;

namespace NQuadro.Assets.Models.Requests;

public sealed class SaveAssetRequest
{
    [Required]
    public string? Name { get; init; }
    public decimal Change { get; init; } = 0.005m;
    [Required]
    public DateTime Start { get; init; }
    [Required]
    public DateTime End { get; init; }
}
