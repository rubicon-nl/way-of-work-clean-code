
using Rubicon.Wow.CleanCode.Example;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using GenericHostConsoleApp;

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


