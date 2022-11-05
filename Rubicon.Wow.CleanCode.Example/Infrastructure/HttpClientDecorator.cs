namespace Rubicon.Wow.CleanCode.Example.Infrastructure;

public class HttpClientDecorator : IDisposable, IHttpClientDecorator
{
    private readonly IHttpClientFactory _httpClientFactory;
    private HttpClient _client;
    private bool _disposedValue;

    public HttpClientDecorator(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient Create(string name)
    {
        _client = _httpClientFactory.CreateClient(name);
        return _client;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _client.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
