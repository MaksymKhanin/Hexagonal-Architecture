// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using FluentAssertions;
using HexagonalApi.Business.Domain;
using HexagonalApi.Business.Ports.Secondary;
using HexagonalApi.Business.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace HexagonalApi.Tests.Business
{
    public class PayloadServiceShould
    {
        private readonly PayloadObject _payload;
        private readonly PayloadService _sut;
        private readonly Mock<IThirdPartyService> _thirdPartyServiceMock = new Mock<IThirdPartyService>();
        private readonly Mock<ICanSavePayload> _icanSavePayloadServiceMock = new Mock<ICanSavePayload>();


        public PayloadServiceShould()
        {
            _payload = GeneralHelpers.GeneratePayloadMock();
            _sut = new PayloadService(_thirdPartyServiceMock.Object, _icanSavePayloadServiceMock.Object);
        }

        [Fact(DisplayName = "no exception should be thrown")]
        public async Task Test1()
        {
            _thirdPartyServiceMock.Setup(x => x.SendAsync(It.IsAny<PayloadObject>()));
            _icanSavePayloadServiceMock.Setup(x => x.CreateAsync(It.IsAny<PayloadObject>()));

            Func<Task> a = async () => await _sut.PostPayload(_payload);

            a.Should().NotThrow();
        }
    }
}
