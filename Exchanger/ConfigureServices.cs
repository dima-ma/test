using Exchanger.ExternalAPI.API;
using Exchanger.Infrastructure.Persistence;
using Exchanger.Options;
using Exchanger.Services;
using Exchanger.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Refit;
using ZymLabs.NSwag.FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddRefitClient<IOpenExchangeRatesAPI>()
            .ConfigureHttpClient(c => { c.BaseAddress = new Uri("https://openexchangerates.org/api"); });
        services.AddScoped<IExchangeService, ExchangeApiService>();
        services.AddScoped<IExchangeConverterFactory, ExchangeConverterFactory>();
        services.AddOptions<OpenExchangeRatesOptions>().BindConfiguration(nameof(OpenExchangeRatesOptions));
        services.AddSingleton<OpenExchangeRatesOptions>(sp =>
            sp.GetRequiredService<IOptions<OpenExchangeRatesOptions>>().Value
        );
        
        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddScoped<FluentValidationSchemaProcessor>(provider =>
        {
            var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            var loggerFactory = provider.GetService<ILoggerFactory>();

            return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
        });

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);
        
        return services;
    }
}
