// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using Azure.Core;
using Azure.Identity;
using HexagonalApi.Adapters.Api.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace EventHandler
{
    public class Program
    {
        public static void Main()
        {
            Debugger.Launch();

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<TokenCredential, DefaultAzureCredential>();
                    services.AddCosmosDbProjection();
                })
                .Build();

            host.Run();
        }
    }
}