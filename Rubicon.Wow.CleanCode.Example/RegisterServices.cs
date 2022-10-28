using Microsoft.Extensions.Configuration;

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
        services.AddSingleton<IConsoleTask, ConsoleTask>();
    }
}