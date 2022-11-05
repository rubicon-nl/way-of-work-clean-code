using GenericHostConsoleApp.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rubicon.Wow.CleanCode.Example.Middleware;

var builder = Host.CreateDefaultBuilder(args);

// configuratie heeft ook default, maar customizing is mogelijk
builder.ConfigureHostConfiguration(configurationBuilder =>
{
    configurationBuilder.AddEnvironmentVariables();
});

// services worden hier gedaan. Verstandig om niet alles in program.cs te registeren
builder.ConfigureServices((hostBuilder, services) =>
{
    new RegisterServices(hostBuilder.Configuration).ConfigureServices(services);
    services.AddHostedService<ConsoleHostedService>();
});

// by default console maar andere log providers ook mogelijk
builder.ConfigureLogging((context, b) =>
 {
     b.AddConsole();
 });

var host = builder.Build();

await host.RunAsync();


