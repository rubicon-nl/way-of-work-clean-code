using Microsoft.Extensions.Logging;
using Rubicon.Wow.CleanCode.Example.Infrastructure;

namespace GenericHostConsoleApp.Infrastructure;

internal sealed class ConsoleHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;

    private Task? _applicationTask;
    private int? _exitCode;
    private IConsoleTask _consoleTask;
    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IConsoleTask _appTask)
    {
        _logger = logger;
        _appLifetime = appLifetime;
        _consoleTask = _appTask;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

        CancellationTokenSource? _cancellationTokenSource = null;

        _appLifetime.ApplicationStarted.Register(() =>
        {
            _logger.LogDebug("Application has started");
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            _applicationTask = Task.Run(async () =>
            {
                try
                {
                    await _consoleTask.ExecuteAsync();

                    _exitCode = 0;
                }
                catch (TaskCanceledException)
                {
                    // This means the application is shutting down, so just swallow this exception
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception!");
                    _exitCode = 1;
                }
                finally
                {
                    // Stop the application once the work is done
                    _appLifetime.StopApplication();
                }
            });
        });

        _appLifetime.ApplicationStopping.Register(() =>
        {
            _logger.LogDebug("Application is stopping");
            _cancellationTokenSource?.Cancel();
        });

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // Wait for the application logic to fully complete any cleanup tasks.
        // Note that this relies on the cancellation token to be properly used in the application.
        if (_applicationTask != null)
        {
            await _applicationTask;
        }

        _logger.LogDebug($"Exiting with return code: {_exitCode}");

        // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
        Environment.ExitCode = _exitCode.GetValueOrDefault(-1);
    }
}