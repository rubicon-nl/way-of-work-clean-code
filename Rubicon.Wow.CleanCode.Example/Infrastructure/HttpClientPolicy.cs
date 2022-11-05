using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace Rubicon.Wow.CleanCode.Example.Infrastructure;

public static class HttpClientPolicy
{
    // retry policy with back of delay. De tijd tussen requests wordt bij elke poging exponentieel langer, startend met 1 seconde en 5 pogingen
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

        return HttpPolicyExtensions
            .HandleTransientHttpError() //alleen bij bepaalde exceptie om onnodige retry te voorkomen
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(delay, onRetryAsync: async (exception, timeDelay, context) =>
            {
                Console.WriteLine($"http call delayd with {timeDelay} failed message {exception.Exception.Message}");
            });
    }

    // circuitbreaker breaker for offlowading
    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
