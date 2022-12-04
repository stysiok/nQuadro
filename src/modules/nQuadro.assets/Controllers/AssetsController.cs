using Microsoft.AspNetCore.Mvc;
using NQuadro.Assets.Logic.Assets.Commands;
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

    [HttpPost]
    public async Task<IActionResult> SaveAsset([FromBody] SaveAsset command)
    {
        await _dispatcher.SendAsync(command);
        return NoContent();
    }

}
