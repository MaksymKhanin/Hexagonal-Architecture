// This code is under Copyright (C) 2021 of Maksym Khanin SAS all right reserved

using HexagonalApi.Adapters.ThirdPartyService;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Xunit;

namespace Adapters
{
    public class ThirdPartyServiceShould
    {
        [Fact(DisplayName = "thirdPartyService should receive payload")]
        public async void Test1()
        {
            ////Arrange
            var expected = "Hello World";
            var mockFactory = new Mock<IHttpClientFactory>();
            var configuration = new HttpConfiguration();
            var clientHandlerStub = new DelegatingHandlerStub((request, cancellationToken) =>
            {
                request.SetConfiguration(configuration);
                var response = request.CreateResponse(HttpStatusCode.OK, expected);
                return Task.FromResult(response);
            });
            var client = new HttpClient(clientHandlerStub);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            IHttpClientFactory factory = mockFactory.Object;



            var sut = new HttpThirdPartyService(factory);
            //var payload = GeneralHelpers.GeneratepayloadMock();

            ////Act
            //var result = await sut.SendAsync(payload);

            ////Assert
            //result.Should().BeTrue();
        }
    }

    public class DelegatingHandlerStub : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;
        public DelegatingHandlerStub()
        {
            _handlerFunc = (request, cancellationToken) => Task.FromResult(request.CreateResponse(HttpStatusCode.OK));
        }

        public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
        {
            _handlerFunc = handlerFunc;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return _handlerFunc(request, cancellationToken);
        }
    }
}
