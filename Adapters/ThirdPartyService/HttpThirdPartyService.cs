// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Domain;
using HexagonalApi.Business.Ports.Secondary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalApi.Adapters.ThirdPartyService
{
    internal class HttpThirdPartyService : IThirdPartyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpThirdPartyService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public async Task<bool> SendAsync(PayloadObject payload)
        {
            var result = await _httpClientFactory.CreateClient("GitHub")
                .GetStringAsync($"users/Elfocrash");

            return await Task.FromResult(true);
        }
    }
}
