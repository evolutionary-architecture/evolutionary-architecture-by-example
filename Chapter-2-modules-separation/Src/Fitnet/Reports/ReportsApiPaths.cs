using EvolutionaryArchitecture.Fitnet.ReusableElements;

namespace EvolutionaryArchitecture.Fitnet.Reports;

using Shared;

internal static class ReportsApiPaths
{
    private const string Reports = $"{ApiPaths.Root}/reports";
    internal const string GenerateNewReport = $"{Reports}/generate";
}