// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.ReadModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace EventHandler
{
    public class RetrievePayloadFunction
    {
        private readonly IStatusProjection _projection;

        public RetrievePayloadFunction(IStatusProjection projection)
        {
            _projection = projection;
        }

        [Function("Function2")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var projection = req.FunctionContext.InstanceServices.GetRequiredService<IStatusProjection>();

            var logger = executionContext.GetLogger("Function2");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = string.Empty;
            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = await streamReader.ReadToEndAsync();
            }

            var queryStatusRequest = JsonConvert.DeserializeObject<QueryStatusRequest>(requestBody);

            var statuses = await new QueryHandler(_projection).Handle(queryStatusRequest);

            return new OkObjectResult(statuses);
        }
    }
}
