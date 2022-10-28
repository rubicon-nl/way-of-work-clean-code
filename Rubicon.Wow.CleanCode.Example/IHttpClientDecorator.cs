namespace Rubicon.Wow.CleanCode.Example;

public interface IHttpClientDecorator
{
    HttpClient Create(string name);
    void Dispose();
}