namespace Bluesky.Net;

using Internals;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

public static class ServiceCollectionExtensions
{

    public static IHttpClientBuilder AddBluesky(
        this IServiceCollection services,
        Action<BlueskyApiOptions>? configurator = null)
    {
        var options = new BlueskyApiOptions();
        configurator?.Invoke(options);
        services.TryAddSingleton(options);
       
        services.AddScoped<IBlueskyApi, BlueskyApi>();
        return services.AddHttpClient(Constants.BlueskyApiClient, (_, client) =>
        {
            client.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
            client.BaseAddress = new Uri(options.Url.TrimEnd('/'));
            client.DefaultRequestHeaders.Add(Constants.HeaderNames.UserAgent, options.UserAgent);
        });
    }
}
