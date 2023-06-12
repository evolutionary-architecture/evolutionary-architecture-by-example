namespace EvolutionaryArchitecture.Fitnet.Common.IntegrationTests.TestEngine.Database;

public interface IDatabaseConfiguration
{
    public Dictionary<string, string?> Get();
}