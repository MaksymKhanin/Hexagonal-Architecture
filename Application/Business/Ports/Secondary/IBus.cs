// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Business.Domain;

namespace HexagonalApi.Business.Ports.Secondary
{
    public interface IBus
    {
        void Publish(PayloadSent payloadSent);
    }
}
