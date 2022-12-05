using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Logic.Assets.Commands;

internal sealed record SaveAsset(string Name, decimal Change, DateTime Start, DateTime End) : ICommand;
