// This code is under Copyright (C) 2021 of Cegid SAS all right reserved

using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Consumer
{
    internal class CosmosRetrieveService : ICanGetPayload
    {
        private readonly Container _container;
        public CosmosRetrieveService(Container container)
        {
            _container = container;
        }
        public async Task<Payload> GetAsync(Guid id)
        {
            PayloadCosmosDto payloadCosmosDto = await _container.ReadItemAsync<PayloadCosmosDto>(id.ToString(), new PartitionKey(id.ToString()));
            var payload = new Payload(payloadCosmosDto.amount, payloadCosmosDto.number);
            return payload;
        }
    }
    internal record PayloadCosmosDto(string id, string number, double amount);
}
