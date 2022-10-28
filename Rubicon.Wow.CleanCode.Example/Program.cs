
using GenericHostConsoleApp;
using Microsoft.Extensions.Configuration;
using Rubicon.Wow.CleanCode.Example;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureHostConfiguration(configurationBuilder =>
{
    configurationBuilder.AddEnvironmentVariables();
});
builder.ConfigureServices((hostBuilder, services) =>
{
    new RegisterServices(hostBuilder.Configuration).ConfigureServices(services);
    services.AddHostedService<ConsoleHostedService>();
});
var host = builder.Build();

await host.RunAsync();


