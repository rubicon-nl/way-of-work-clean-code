namespace Rubicon.Wow.CleanCode.Example.Infrastructure;

public interface IHttpClientDecorator
{
    HttpClient Create(string name);
    void Dispose();
}