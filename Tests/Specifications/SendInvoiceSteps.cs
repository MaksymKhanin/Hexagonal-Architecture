using Adapters.Helpers;
using AutoMapper;
using FluentAssertions;
using HexagonalApi.Adapters.Api.Controllers;
using HexagonalApi.Business.Commands;
using HexagonalApi.Business.Domain;
using HexagonalApi.Business.Ports.Secondary;
using HexagonalApi.ReadModel;
using HexagonalApi.Tests.Business;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace HexagonalApi.Tests.Specifications
{
    [Binding]
    public class SendpayloadSteps
    {
        private PayloadObject? _payload;
        private bool _payloadTakenIntoAccount;
        private readonly ApiController _sut;
        private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly IStatusProjection _projection = new InMemoryStatusProjection();

        public SendpayloadSteps()
        {
            _sut = new ApiController(_mediatorMock.Object, _mapperMock.Object);
        }

        [Given(@"I have created my payload")]
        public void GivenIHaveCreatedMypayload()
        {
            _payload = GeneralHelpers.GeneratePayloadMock();
        }

        [When(@"I send my payload to the payload platform")]
        public async Task WhenISendMypayloadToThepayloadPlatform() 
        {
            var sendpayloadCommandDto = StubsFactory.SendPayloadCommandDtoValidStub();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<SendPayloadCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Verifiable("payload was not sent.");

            _payloadTakenIntoAccount = (bool)(await _sut.Post(sendpayloadCommandDto) as OkObjectResult)?.Value;
        }

        [Then(@"the payload platform has taken my payload into account")]
        public async void ThenThepayloadPlatformHasTakenMypayloadIntoAccount()
        {
            var request = new QueryStatusRequest();
            var statuses = await new QueryHandler(_projection).Handle(request);
            statuses.Should().ContainSingle();
        }
    }

    internal class InMemoryStatusProjection : IStatusProjection, IBus
    {
        private readonly ICollection<StatusDto> _statuses = new List<StatusDto>();

        public Task<HexagonalApi.ReadModel.QueryStatus[]> List(Guid id)
        {
            _statuses.Add(new StatusDto() { Status = "Success" });
            var statuses = _statuses.Select(entity => new HexagonalApi.ReadModel.QueryStatus()).ToArray();
            return Task.FromResult(statuses);
        }
        public Task Save(PayloadSaved status) => throw new NotImplementedException();

        public void Publish(PayloadSent payloadSent) => _statuses.Add(new StatusDto());
    }
}
