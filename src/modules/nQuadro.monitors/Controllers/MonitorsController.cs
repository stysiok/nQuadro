using Microsoft.AspNetCore.Mvc;
using NQuadro.Monitors.Logic.Monitors.Commands;
using NQuadro.Monitors.Logic.Monitors.Queries;
using NQuadro.Monitors.Models.Requests;
using NQuadro.Shared.CQRS;

namespace NQuadro.Monitors.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class MonitorsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    public MonitorsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{asset}")]
    public async Task<ActionResult<bool>> IsMonitoringOn([FromRoute] string asset)
    {
        var query = new IsMonitoringOn(asset);
        return await _dispatcher.QueryAsync(query);
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartMonitoringAssetAsync([FromBody] StartMonitoringAssetRequest body)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(body.Name))
            return BadRequest(ModelState.ValidationState);

        var command = new StartMonitoringAsset(body.Name);
        await _dispatcher.SendAsync(command);
        return Accepted();
    }

    [HttpPost("stop")]
    public async Task<IActionResult> StopMonitoringAssetAsync([FromBody] StopMonitoringAssetRequest body)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(body.Name))
            return BadRequest(ModelState.ValidationState);

        var command = new StopMonitoringAsset(body.Name);
        await _dispatcher.SendAsync(command);
        return Accepted();
    }
}
