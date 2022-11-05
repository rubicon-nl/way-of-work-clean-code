using Moq;
using Rubicon.Wow.CleanCode.Data;
using Rubicon.Wow.CleanCode.Example.Infrastructure;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rubicon.Wow.CleanCode.Example.Tests.TestSupport;
internal static class HttpFactoryStub
{
    public static async Task<HttpClientDecorator> GetHttpDecorator(DisneyCharacters responseData)
    {
        var httpResponseContent = await JsonSerialization.SerializeAsync(responseData);
        return new HttpClientDecorator(GetHttpClientFactory(httpResponseContent));
    }

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
