// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using Microsoft.Azure.Cosmos;
using System;
using System.Threading.Tasks;

namespace HexagonalApi.ReadModel
{
    internal class CosmosDbStatusProjection : IStatusProjection
    {
        private readonly Container _container;

        public CosmosDbStatusProjection(Container container)
        {
            _container = container;
        }
        public async Task<QueryStatus[]> List(Guid id)
        {
            PayloadCosmosDto payloadCosmosDto = await _container.ReadItemAsync<PayloadCosmosDto>(id.ToString(), new PartitionKey(id.ToString()));
            var queryStatuses = payloadCosmosDto.queryStatuses;
            return queryStatuses;
        }
        public Task Save(PayloadSaved payloadSaved)
        {
            var dto = new PayloadCosmosDto(payloadSaved.id.ToString(), payloadSaved.statuses);
            return _container.CreateItemAsync(dto);
        }

    }
    internal record PayloadCosmosDto(string id, QueryStatus[] queryStatuses);
}
