// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Ports.Secondary;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddServiceBusBus(this IServiceCollection services) =>
            services.AddSingleton<IBus, ServiceBusBus>();
    }
}
