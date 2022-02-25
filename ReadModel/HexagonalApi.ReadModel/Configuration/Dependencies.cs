// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.ReadModel;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HexagonalApi.Adapters.Api.Configuration
{
    public static class Dependencies
    {
        private const string ContainerId = "ReadContainer";
        private const string DatabaseId = "ReadDb";
        private const string ConnectionStringNullExceptionMessage = "Connection string should be set";
        private const string CosmosEmulatorConnectionEnvVariable = "COSMOS_EMULATOR_CONNECTION_STRING";

        public static IServiceCollection AddCosmosDbProjection(this IServiceCollection services) =>
            services.AddSingleton(sp =>
                {
                    var configuration = sp.GetRequiredService<IConfiguration>();

                    string connectionString = Environment.GetEnvironmentVariable(CosmosEmulatorConnectionEnvVariable)
                        ?? throw new InvalidOperationException(ConnectionStringNullExceptionMessage);

                    var builder = new CosmosClientBuilder(connectionString);
                    return builder.Build().GetDatabase(DatabaseId);
                })
                .AddTransient<IStatusProjection>(sp =>
                {
                    var configuration = sp.GetRequiredService<IConfiguration>();

                    var dataBase = sp.GetRequiredService<Database>();

                    return new CosmosDbStatusProjection(dataBase.GetContainer(ContainerId));
                });
    }
}