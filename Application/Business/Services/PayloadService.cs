// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Domain;
using HexagonalApi.Business.Ports.Secondary;
using System.Threading.Tasks;

namespace HexagonalApi.Business.Services
{
    public class PayloadService
    {
        private readonly IThirdPartyService _thirdPartyService;
        private readonly ICanSavePayload _cosmosService;

        public PayloadService(IThirdPartyService thirdPartyService, ICanSavePayload cosmosService) =>
            (_thirdPartyService, _cosmosService) = (thirdPartyService, cosmosService);

        public async Task<string> PostPayload(PayloadObject payload)
        {
            var isSentTothirdPartyService = await _thirdPartyService.SendAsync(payload);
            return await _cosmosService.CreateAsync(payload);
        }
    }
}

