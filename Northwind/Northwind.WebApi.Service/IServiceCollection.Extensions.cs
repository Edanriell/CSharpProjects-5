using Microsoft.AspNetCore.HttpLogging;
using AspNetCoreRateLimit;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(options =>
        {
            options.RequestHeaders.Add("Origin");

            options.RequestHeaders.Add("X-Client-Id");
            options.ResponseHeaders.Add("Retry-After");

            options.LoggingFields = HttpLoggingFields.All;
        });
        return services;
    }

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                name: "Northwind.Mvc.Policy",
                policy =>
                {
                    policy.WithOrigins("https://localhost:5082");
                }
            );
        });
        return services;
    }

    public static IServiceCollection AddCustomRateLimiting(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.AddMemoryCache();
        services.AddInMemoryRateLimiting();

        services.Configure<ClientRateLimitOptions>(configuration.GetSection("ClientRateLimiting"));

        services.Configure<ClientRateLimitPolicies>(
            configuration.GetSection("ClientRateLimitPolicies")
        );

        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return services;
    }
}
