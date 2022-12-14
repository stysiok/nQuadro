using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Logic.Assets.Commands;

internal sealed record SaveAsset(string Name, double Change, DateTime End) : ICommand;
