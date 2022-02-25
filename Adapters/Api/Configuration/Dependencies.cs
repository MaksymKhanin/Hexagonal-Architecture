// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using FluentValidation;
using HexagonalApi.Business.PipelineBehavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HexagonalApi.Adapters.Api.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddMediatorPipelineBehavior(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            var assembly = AppDomain.CurrentDomain.Load("HexagonalApi.Business");
            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
            return services;
        }
    }
}
