using Microsoft.AspNetCore.Mvc;
using NQuadro.Assets.Logic.Assets.Commands;
using NQuadro.Assets.Logic.Assets.Queries;
using NQuadro.Assets.Models.DTOs;
using NQuadro.Assets.Models.Requests;
using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class AssetsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    public AssetsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetDTO>>> GetAssetsAsync()
    {
        var result = await _dispatcher.QueryAsync(new GetAssets());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAssetAsync([FromBody] SaveAssetRequest body)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.ValidationState);
        if (string.IsNullOrWhiteSpace(body.Name)) return BadRequest("Name has to be different than null or white spaces");
        var command = new SaveAsset(body.Name, body.Change, body.Start, body.End);
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

}
