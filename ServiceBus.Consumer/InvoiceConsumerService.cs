// This code is under Copyright (C) 2021 of Cegid SAS all right reserved

using AutoMapper;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ServiceBus.Consumer.DTOs;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Consumer
{
    class InvoiceConsumerService : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ICanGetInvoice _cosmosRetrieveService;

        public InvoiceConsumerService(ISubscriptionClient subscriptionClient, IMediator mediator, IMapper mapper, ICanGetInvoice cosmosRetrieveService)
        {
            _subscriptionClient = subscriptionClient;
            _mediator = mediator;
            _mapper = mapper;
            _cosmosRetrieveService = cosmosRetrieveService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler((message, token) =>
                {
                    var getInvoiceRequestDto =
                        JsonConvert.DeserializeObject<GetInvoiceRequestDto>(Encoding.UTF8.GetString(message.Body));


                    //_mediator.Send(_mapper.Map<GetInvoiceRequest>(getInvoiceRequestDto));
                    _cosmosRetrieveService.GetAsync(getInvoiceRequestDto.id);

                    return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
                },
                new MessageHandlerOptions(args => Task.CompletedTask)
                {
                    AutoComplete = false,
                    MaxConcurrentCalls = 1
                });
            return Task.CompletedTask;
        }
    }
}
