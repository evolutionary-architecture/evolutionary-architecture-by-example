namespace EvolutionaryArchitecture.Fitnet.Common.Validation.Requests.Logging;

using System.Text;

internal sealed class RequestValidationLogger : IRequestValidationLogger
{
    private readonly ILogger _logger;

    public RequestValidationLogger(ILogger<RequestValidationLogger> logger) => _logger = logger;

    public void LogValidationErrors(IDictionary<string, string[]> errors)
    {
        var errorLogMessageBuilder = new StringBuilder()
            .AppendLine("Request failed validation. Found the following issues:");

        foreach (var error in errors) 
            errorLogMessageBuilder.AppendLine($"Property {error.Key} has errors: {string.Join(",", error.Value)}");
        
        var errorMessage = errorLogMessageBuilder.ToString();
        _logger.LogError(errorMessage);
    }
}