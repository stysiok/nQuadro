using NQuadro.Shared.CQRS;

namespace NQuadro.Monitors.Logic.Monitors.Commands;

public record StopMonitoringAsset(string Name) : ICommand;
