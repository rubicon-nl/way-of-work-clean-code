using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Rubicon.Wow.CleanCode.Example.Tests.TestSupport;

public class DelegatingHandlerStub : DelegatingHandler
{
    private Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

    public string ExpectedResult { get; set; }
    public string ContentType { get; set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.OK);
        _handlerFunc = (request, cancellationToken) => Task.FromResult(msg);
        msg.Content = new StringContent(ExpectedResult, System.Text.Encoding.UTF8, ContentType);

        return _handlerFunc(request, cancellationToken);
    }
}
