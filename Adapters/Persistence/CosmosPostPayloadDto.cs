// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using Cosmonaut.Attributes;
using Newtonsoft.Json;
using System;

namespace HexagonalApi.Adapters.Persistence
{
    [CosmosCollection("posts")]
    public class CosmosPostPayloadDto
    {
        [CosmosPartitionKey]
        [JsonProperty("id")]
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public double Amount { get; set; }

        

    }
}

