// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Ports.Secondary;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;

namespace HexagonalApi.Adapters.ThirdPartyService.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddHttpThirdPartyService(this IServiceCollection services)
        {
            services.AddTransient<IThirdPartyService, HttpThirdPartyService>();

            services.AddHttpClient("GitHub", client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryExample");
            })
            .AddTransientHttpErrorPolicy(x =>
                x.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

            return services;
        }
    }
}
