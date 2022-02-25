// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Commands;
using HexagonalApi.Business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexagonalApi.Tests.Business
{
    public static class GeneralHelpers
    {
        public static PayloadObject GeneratePayloadMock()
        {
            return new PayloadObject(5, "#99999999");
        }
        public static PayloadObject GeneratePayloadMockWithWrongNumber()
        {
            return new PayloadObject(0, "#99999999");
        }

        public static SendPayloadCommand GenerateSendPayloadCommandMock()
        {
            var command = new SendPayloadCommand("#99999999", 7);

            return command;
        }
    }
}
