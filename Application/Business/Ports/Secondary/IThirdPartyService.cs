// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Domain;
using System.Threading.Tasks;

namespace HexagonalApi.Business.Ports.Secondary
{
    public interface IThirdPartyService
    {
        Task<bool> SendAsync(PayloadObject payload);
    }
}
