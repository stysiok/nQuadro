using System.ComponentModel.DataAnnotations;

namespace NQuadro.Assets.Models.Requests;

public sealed class SaveAssetRequest
{
    [Required]
    public string? Name { get; init; }
}
