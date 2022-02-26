// This code is under Copyright (C) 2021 of Cegid SAS all right reserved

using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Consumer.Configuration
{
    public static class Dependencies
    {

        public static IServiceCollection AddCosmosRetrieveService(this IServiceCollection services) =>
            services.AddTransient<ICanGetPayload, CosmosRetrieveService>();
    }
}
