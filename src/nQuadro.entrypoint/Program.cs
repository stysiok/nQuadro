using NQuadro.Assets;
using NQuadro.Monitors;
using NQuadro.Notifications;
using NQuadro.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSharedComponents(builder.Configuration)
    .AddAssetsModule()
    .AddMonitorsModule(builder.Configuration)
    .AddNotificationsModule();

var app = builder.Build();

app.UseRouting();
app.UseCors(conf =>
{
    conf.AllowAnyOrigin();
    conf.AllowAnyHeader();
    conf.AllowAnyMethod();
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("Hello world!"));
});

app.Run();
