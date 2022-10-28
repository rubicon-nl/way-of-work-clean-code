using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace Rubicon.Wow.CleanCode.Example;
public class RegisterServices
{
    public IConfiguration Configuration { get; }

    public RegisterServices(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();

        services.AddHttpClient("Disney", httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://aapi.disneyapi.dev/"); // TODO: wrong api?

            /* other things here
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            */
        }).AddPolicyHandler(HttpClientPolicy.GetRetryPolicy()); ;

        services.AddSingleton<IConsoleTask, ConsoleTask>();
        services.AddSingleton<IHttpClientDecorator, HttpClientDecorator>();
        services.AddTransient<IDisneyCharacterService, DisneyCharacterService>();
    }
}