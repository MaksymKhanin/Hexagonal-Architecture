// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using Adapters.Helpers;
using AutoMapper;
using HexagonalApi.Adapters.Api.Controllers;
using HexagonalApi.Business.Commands;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Adapters
{
    public class ControllerShould
    {
        private readonly ApiController _sut;
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public ControllerShould()
        {
            _sut = new ApiController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact(DisplayName = "if payload came, no exception should occure")]
        public async Task Test1()
        {
            //Arrange
            var sendPayloadCommandDto = StubsFactory.SendPayloadCommandDtoValidStub();

            //Act
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SendPayloadCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Verifiable("payload was not sent.");
                
            Func<Task> f = async () => await _sut.Post(sendPayloadCommandDto);

            //Assert
            f.Should().NotThrow();
        }
    }
}
