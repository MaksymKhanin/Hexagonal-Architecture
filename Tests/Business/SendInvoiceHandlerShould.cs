// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using AutoMapper;
using FluentAssertions;
using HexagonalApi.Business.Domain;
using HexagonalApi.Business.Handling;
using HexagonalApi.Business.Ports.Secondary;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace HexagonalApi.Tests.Business
{
    public class SendpayloadHandlerShould
    {
        private readonly Mock<ILogger<SendPayloadHandler>> _loggerMock = new Mock<ILogger<SendPayloadHandler>>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IThirdPartyService> _thirdPartyServiceMock = new Mock<IThirdPartyService>();
        private readonly Mock<ICanSavePayload> _icanSavepayloadServiceMock = new Mock<ICanSavePayload>();
        private readonly Mock<IBus> _iBusMock = new Mock<IBus>();
        private readonly SendPayloadHandler _sut;

        public SendpayloadHandlerShould()
        {
            _sut = new SendPayloadHandler(_loggerMock.Object, _thirdPartyServiceMock.Object, _icanSavepayloadServiceMock.Object, _iBusMock.Object);
        }

        [Fact(DisplayName = "SendpayloadHandler should not throw exceptions")]
        public void Test1()
        {
            _thirdPartyServiceMock.Setup(x => x.SendAsync(It.IsAny<PayloadObject>())).ReturnsAsync(true);
            _icanSavepayloadServiceMock.Setup(x => x.CreateAsync(It.IsAny<PayloadObject>())).ReturnsAsync(Guid.NewGuid().ToString());
            _iBusMock.Setup(x => x.Publish(It.IsAny<PayloadSent>()));
            Func<Task<bool>> f = async () => await _sut.Handle(GeneralHelpers.GenerateSendPayloadCommandMock(), default(CancellationToken));

            f.Should().NotThrow();
        }

        [Fact(DisplayName = "When sending payload it should be taken into account")]
        public async void Test2()
        {
            _thirdPartyServiceMock.Setup(x => x.SendAsync(It.IsAny<PayloadObject>())).ReturnsAsync(true);
            _icanSavepayloadServiceMock.Setup(x => x.CreateAsync(It.IsAny<PayloadObject>())).ReturnsAsync(Guid.NewGuid().ToString());
            _iBusMock.Setup(x => x.Publish(It.IsAny<PayloadSent>()));
            var res = await _sut.Handle(GeneralHelpers.GenerateSendPayloadCommandMock(), default(CancellationToken));

            res.Should().BeTrue();
        }
    }
}
