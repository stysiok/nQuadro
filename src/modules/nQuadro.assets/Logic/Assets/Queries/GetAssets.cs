using NQuadro.Assets.Models.DTOs;
using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Logic.Assets.Queries;

internal sealed record GetAssets() : IQuery<IEnumerable<AssetDTO>>;
