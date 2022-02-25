// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HexagonalApi.Adapters.Api.Commands
{
    public record SendPayloadCommandDto(string Number, double Amount);
}
