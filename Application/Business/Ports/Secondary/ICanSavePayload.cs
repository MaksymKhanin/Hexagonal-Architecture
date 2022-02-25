// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalApi.Business.Ports.Secondary
{
    public interface ICanSavePayload
    {
        public Task<string> CreateAsync(PayloadObject payload);
    }
}

