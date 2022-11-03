using Moq;
using System;
using System.Net.Http;

namespace Rubicon.Wow.CleanCode.Example.Tests.TestSupport;
internal static class HttpFactoryStub
{
    public static IHttpClientFactory GetHttpClientFactory(string httpResponseContent)
    {
        var clientHandlerStub = new DelegatingHandlerStub();
        clientHandlerStub.ExpectedResult = httpResponseContent;
        clientHandlerStub.ContentType = "application/json";

        var client = new HttpClient(clientHandlerStub);
        client.BaseAddress = new Uri("https://api.disneyapi.dev/");
        Mock<IHttpClientFactory> mockFactory = new();
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        IHttpClientFactory factory = mockFactory.Object;

        return factory;
    }
}
