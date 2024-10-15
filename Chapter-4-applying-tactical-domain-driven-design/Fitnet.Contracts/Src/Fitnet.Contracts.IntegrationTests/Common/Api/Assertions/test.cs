namespace EvolutionaryArchitecture.Fitnet.Contracts.IntegrationTests.Common.Api.Assertions;

internal static class HttpResponseMessageExtensions
{
    public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response) => response.IsSuccessStatusCode.Should().BeTrue($"Expected a successful status code but got {response.StatusCode}. Response content: {await response.Content.ReadAsStringAsync()}");
}
