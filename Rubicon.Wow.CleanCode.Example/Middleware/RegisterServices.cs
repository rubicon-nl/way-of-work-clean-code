using Microsoft.Extensions.Configuration;
using Rubicon.Wow.CleanCode.Example.Infrastructure;

namespace Rubicon.Wow.CleanCode.Example.Middleware;
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
            httpClient.BaseAddress = new Uri("https://api.disneyapi.dev/");

            /* other things here
            httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            */
        }).AddPolicyHandler(HttpClientPolicy.GetRetryPolicy()); ;

        services.AddSingleton<IConsoleTask, ConsoleTask>();
        services.AddSingleton<IOutputWriter, OutputWriter>();
        services.AddSingleton<IHttpClientDecorator, HttpClientDecorator>();
        services.AddTransient<IDisneyCharacterRepository, DisneyCharacterRepository>();
    }
}