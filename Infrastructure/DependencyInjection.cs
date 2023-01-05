using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Nasa.Domain.Settings;
using Nasa.Infrastructure.HttpClients.NeoWs;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddHttpClient<FeedHttpClient>(client => client.BaseAddress = new Uri(NasaSettings.FeedHttpClientBaseAddress));

        return services;
    }
}
