using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Logic.Assets.Commands;

internal sealed record DeleteAsset(string Name) : ICommand;