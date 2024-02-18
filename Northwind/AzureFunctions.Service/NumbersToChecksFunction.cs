using Humanizer;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctions.Service;

public class NumbersToChecksFunction
{
    private readonly ILogger _logger;

    public NumbersToChecksFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<NumbersToChecksFunction>();
    }

    [Function(nameof(NumbersToChecksFunction))]
    [QueueOutput("checksQueue")]
    public string Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            HttpRequestData request
    )
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string? amount = request.Query["amount"];

        if (long.TryParse(amount, out long number))
        {
            return number.ToWords();
        }
        else
        {
            return $"Failed to parse: {amount}";
        }
    }
}
