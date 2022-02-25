// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Adapters.Api.Configuration;
using HexagonalApi.Adapters.Persistence.Configuration;
using HexagonalApi.Adapters.Persistence;
using HexagonalApi.Adapters.ThirdPartyService.Configuration;
using HexagonalApi.Business.Ports.Secondary;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using System;
using Messaging.Configuration;

namespace HexagonalApi.Adapters.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HexagonalApi.Api", Version = "v1" });
            });

            
            services.AddCosmosDbPayloadPersistence();
            services.AddServiceBusBus();
            services.AddHttpThirdPartyService();
            services.AddMediatorPipelineBehavior();
            services.AddAutoMapper(typeof(Startup));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HexagonalApi.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
