// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HexagonalApi.Adapters.Api
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
