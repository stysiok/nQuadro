using NQuadro.Assets;
using NQuadro.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAssetsModule()
    .AddSharedComponents(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", ctx => ctx.Response.WriteAsync("Hello world!"));
});

app.Run();
