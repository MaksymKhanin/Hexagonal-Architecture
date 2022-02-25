// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using AutoMapper;
using HexagonalApi.Adapters.Api.Commands;
using HexagonalApi.Business.Commands;

namespace HexagonalApi.Adapters.Api.Mapping
{
    public class RequestToDomainMapping : Profile
    {
        public RequestToDomainMapping()
        {
            CreateMap<SendPayloadCommandDto, SendPayloadCommand>();
        }
    }
}
