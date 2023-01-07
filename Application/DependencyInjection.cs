using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Nasa.Application.Behaviors;
using Nasa.Domain.Settings;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddOptions<NasaSettings>()
                .BindConfiguration(NasaSettings.SectionName)
                .Validate(settings => string.IsNullOrWhiteSpace(settings.ApiKey) is false, $"'{nameof(NasaSettings)}.{nameof(NasaSettings.ApiKey)}' is required.")
                .ValidateOnStart();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddInfrastructure(configuration, environment);

        services.AddMemoryCache();

        return services;
    }
}
