using NQuadro.Assets;
using NQuadro.Monitors;
using NQuadro.Notifications;
using NQuadro.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSharedComponents(builder.Configuration)
    .AddAssetsModule()
    .AddMonitorsModule()
    .AddNotificationsModule();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("Hello world!"));
});

app.Run();
