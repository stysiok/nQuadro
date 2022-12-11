using NQuadro.Shared.CQRS;

namespace NQuadro.Monitors.Logic.Monitors.Queries;

internal sealed record IsMonitoringOn(string Asset) : IQuery<bool>;