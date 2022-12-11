using Microsoft.AspNetCore.Mvc;
using NQuadro.Notifications.Logic.Notifications.Commands;
using NQuadro.Notifications.Logic.Notifications.Queries;
using NQuadro.Notifications.Models.DTOs;
using NQuadro.Shared.CQRS;

namespace NQuadro.Assets.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    public NotificationsController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{asset}")]
    public async Task<ActionResult<NotificationDTO>> GetNotificationSettings([FromRoute] string asset)
    {
        var notificationSettings = await _dispatcher.QueryAsync(new GetNotifications(asset));
        return Ok(notificationSettings);
    }

    [HttpPost("{asset}")]
    public async Task<ActionResult<NotificationDTO>> SetNotificationSettings([FromRoute] string asset, [FromBody] NotificationDTO notificationDTO)
    {
        await _dispatcher.SendAsync(new SetNotifications(asset, notificationDTO));
        return Accepted();
    }
}
