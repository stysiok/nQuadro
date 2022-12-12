using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NQuadro.Notifications.Models.Options;
using SendGrid.Extensions.DependencyInjection;

namespace NQuadro.Notifications.Services.SendGrid;

internal static class EmailExtensions
{
    internal static IServiceCollection AddEmailNotificationService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        var sendGridOptionsSection = configuration.GetSection("SendGrid");
        if (!sendGridOptionsSection.Exists()) return serviceCollection;

        serviceCollection.AddSendGrid(options =>
        {
            options.ApiKey = sendGridOptionsSection.GetValue<string>("ApiKey");
        });

        return serviceCollection
            .Configure<SendGridOptions>(sendGridOptionsSection)
            .AddSingleton<INotificationService, EmailNotificationService>();
    }
}
