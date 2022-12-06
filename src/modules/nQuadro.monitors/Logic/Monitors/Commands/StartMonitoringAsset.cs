using NQuadro.Shared.CQRS;

namespace NQuadro.Monitors.Logic.Monitors.Commands;

public record StartMonitoringAsset(string Name) : ICommand;
