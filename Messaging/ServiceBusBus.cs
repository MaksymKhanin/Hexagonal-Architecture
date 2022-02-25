using Azure.Messaging.ServiceBus;
using HexagonalApi.Business.Domain;
using HexagonalApi.Business.Ports.Secondary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace Messaging
{
    public class ServiceBusBus : IBus
    {
        private readonly ILogger _logger;
        private const string TOPIC_PATH = "mytopic";
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _clientSender;

        public ServiceBusBus(IConfiguration configuration, ILogger<ServiceBusBus> logger)
        {
            _logger = logger;
            var connectionString = configuration.GetConnectionString("ServiceBusConnectionString");
            _client = new ServiceBusClient(connectionString);
            _clientSender = _client.CreateSender(TOPIC_PATH);
        }

        public async void Publish(PayloadSent payloadSent)
        {
            string messagePayload = JsonSerializer.Serialize(payloadSent);
            ServiceBusMessage message = new ServiceBusMessage(messagePayload);

            message.ApplicationProperties.Add("payloadSent", payloadSent);

            try
            {
                await _clientSender.SendMessageAsync(message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
