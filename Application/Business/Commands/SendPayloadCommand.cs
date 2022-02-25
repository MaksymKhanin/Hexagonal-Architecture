// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using MediatR;

namespace HexagonalApi.Business.Commands
{
    public record SendPayloadCommand(string Number, double Amount) : IRequest<bool>;
}
