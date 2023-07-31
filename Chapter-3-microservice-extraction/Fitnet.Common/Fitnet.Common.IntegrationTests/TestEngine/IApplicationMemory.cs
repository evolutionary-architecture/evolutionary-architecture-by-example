namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine;

public interface IApplicationFactory
{
    IServiceProvider ServiceProvider { get; }
}