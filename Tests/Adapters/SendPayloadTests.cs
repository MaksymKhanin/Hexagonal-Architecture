// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using Adapters.Helpers;
using FluentAssertions;
using HexagonalApi.Adapters.Persistence.Configuration;
using HexagonalApi.Business.Ports.Secondary;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using Xunit;

namespace Adapters
{
    public class SendPayloadTests
    {
        private ICanSavePayload _sut => _host.Services.GetRequiredService<ICanSavePayload>();
        private Container _container => _host.Services.GetRequiredService<Database>().GetContainer("WriteContainer");
        private readonly IHost _host;

        public SendPayloadTests() => _host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .Build();

        private void ConfigureAppConfiguration(IConfigurationBuilder builder) =>
            builder.AddInMemoryCollection(new Dictionary<string, string> 
            {
                { "CosmosSettings:ConnectionString", Environment.GetEnvironmentVariable("COSMOS_EMULATOR_CONNECTION_STRING") 
                    ?? throw new InvalidOperationException("connection string should be set")},
                { "CosmosSettings:WriteDb:DatabaseId", "WriteDb"},
                { "CosmosSettings:WriteDb:ContainerId", "WriteContainer"},
            });

        private void ConfigureServices(IServiceCollection builder) => builder.AddCosmosDbPayloadPersistence();

        [Fact]
        public async void Test1()
        {
            var payload = StubsFactory.PayloadValidStub();
            var result = await _sut.CreateAsync(payload);

            var iterator = _container.GetItemQueryIterator<payloadCosmosDto>();
            var items = new List<payloadCosmosDto>();

            while (iterator.HasMoreResults)
            {
                var results = await iterator.ReadNextAsync();
                items.AddRange(results);
            }
            items.Should().Contain(new payloadCosmosDto("#15274639", 64344));
        }
    }

    internal record payloadCosmosDto(string number, double amount);
}
