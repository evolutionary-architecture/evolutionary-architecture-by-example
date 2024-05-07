namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.AttachAnnexToBindingContract;

using EvolutionaryArchitecture.Fitnet.Contracts.Api.AttachAnnexToBindingContract;

internal sealed class AttachAnnexToBindingContractRequestFaker : Faker<AttachAnnexToBindingContractRequest>
{
    internal AttachAnnexToBindingContractRequestFaker(DateTimeOffset? validFrom) => CustomInstantiator(faker =>
        new AttachAnnexToBindingContractRequest(validFrom ?? faker.Date.RecentOffset().ToUniversalTime())
    );
}
