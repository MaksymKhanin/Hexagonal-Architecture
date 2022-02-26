// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Adapters.Api.Commands;
using HexagonalApi.Business.Domain;

namespace Adapters.Helpers
{
    public static class StubsFactory
    {
        private const string Validnumber = "#15274639";
        private const double ValidAmount = 64344;


        public static SendPayloadCommandDto SendPayloadCommandDtoValidStub()
        {
            return new SendPayloadCommandDto(Validnumber, ValidAmount);
        }

        public static PayloadObject PayloadValidStub()
        {
            return new PayloadObject(ValidAmount, Validnumber);
        }
    }
}
