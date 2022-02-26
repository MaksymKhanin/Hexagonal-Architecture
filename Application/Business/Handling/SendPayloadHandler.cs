// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Commands;
using HexagonalApi.Business.Domain;
using HexagonalApi.Business.Ports.Secondary;
using HexagonalApi.Business.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HexagonalApi.Business.Handling
{
    internal class SendPayloadHandler : IRequestHandler<SendPayloadCommand, bool>
    {
        private readonly ILogger<SendPayloadHandler> _logger;
        private readonly PayloadService _payloadService;
        private readonly IThirdPartyService _thirdPartyService;
        private readonly ICanSavePayload _cosmosService;
        private readonly IBus _bus;

        public SendPayloadHandler(ILogger<SendPayloadHandler> logger, IThirdPartyService thirdPartyService, ICanSavePayload cosmosService, IBus bus)
        {
            _logger = logger;
            _thirdPartyService = thirdPartyService;
            _cosmosService = cosmosService;
            _payloadService = new PayloadService(_thirdPartyService, _cosmosService);
            _bus = bus;
        }

        public async Task<bool> Handle(SendPayloadCommand command, CancellationToken cancellationToken)
        {
            var payload = new PayloadObject(command.Amount, command.Number);

            var payloadId = await _payloadService.PostPayload(payload);

            _bus.Publish(new PayloadSent());

            _logger.LogInformation($"payload: {payload.Number} was sent to ThirdPartyService");

            return true;
        }
    }
}
