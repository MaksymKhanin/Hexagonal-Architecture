// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Ports.Secondary;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HexagonalApi.Adapters.Persistence.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddCosmosDbPayloadPersistence(this IServiceCollection services) =>
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                string connectionString = Environment.GetEnvironmentVariable("COSMOS_EMULATOR_CONNECTION_STRING")
                    ?? throw new InvalidOperationException("connection string should be set");
                string databaseId = configuration["CosmosSettings:WriteDb:DatabaseId"];
                string containerId = configuration["CosmosSettings:WriteDb:ContainerId"];


                var builder = new CosmosClientBuilder(connectionString);
                return builder.Build().GetDatabase(databaseId);
            })
            .AddTransient<ICanSavePayload>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                var dataBase = sp.GetRequiredService<Database>();
                string containerId = configuration["CosmosSettings:WriteDb:ContainerId"];

                return new CosmosService(dataBase.GetContainer(containerId));
            });
    }
}
