// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using Cosmonaut;
using HexagonalApi.Business.Domain;
using HexagonalApi.Adapters.Persistence.Configuration;
using HexagonalApi.Business.Ports.Secondary;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalApi.Adapters.Persistence
{
    internal class CosmosService : ICanSavePayload
    {
        private readonly Container _container;
        public CosmosService(Container container)
        {
            _container = container;
        }
        public async Task<string> CreateAsync(PayloadObject payload)
        {
            var dto = new PayloadCosmosDto(Guid.NewGuid().ToString(), payload.Number, payload.Amount);
            await _container.CreateItemAsync(dto);
            return dto.id;
        }   
    }   
    internal record PayloadCosmosDto(string id, string number, double amount);
}

