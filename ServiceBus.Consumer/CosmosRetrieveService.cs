// This code is under Copyright (C) 2021 of Cegid SAS all right reserved

using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Consumer
{
    internal class CosmosRetrieveService : ICanGetInvoice
    {
        private readonly Container _container;
        public CosmosRetrieveService(Container container)
        {
            _container = container;
        }
        public async Task<Invoice> GetAsync(Guid id)
        {
            InvoiceCosmosDto invoiceCosmosDto = await _container.ReadItemAsync<InvoiceCosmosDto>(id.ToString(), new PartitionKey(id.ToString()));
            var invoice = new Invoice(invoiceCosmosDto.amount, invoiceCosmosDto.siret);
            return invoice;
        }
    }
    internal record InvoiceCosmosDto(string id, string siret, double amount);
}
