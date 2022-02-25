// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.ReadModel;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventHandler
{
    public static class Function1
    {
        [Function("Function1")]
        public static Task Run([ServiceBusTrigger("myqueue")] string myQueueItem, FunctionContext context)
        {
            var logger = context.GetLogger("Function1");
            logger.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var projection = context.InstanceServices.GetRequiredService<IStatusProjection>();
            var payloadSent = JsonSerializer.Deserialize<PayloadSent>(myQueueItem);
            var payloadSaved = new PayloadSaved(payloadSent.Id, payloadSent.Statuses);

            return projection.Save(payloadSaved);
        }
    }
    internal record PayloadSent(Guid Id, QueryStatus[] Statuses);
}
