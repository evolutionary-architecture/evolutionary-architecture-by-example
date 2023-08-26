namespace EvolutionaryArchitecture.Fitnet.Common.Validation.Requests.Logging;

internal interface IRequestValidationLogger
{
    void LogValidationErrors(IDictionary<string, string[]> errors);
}