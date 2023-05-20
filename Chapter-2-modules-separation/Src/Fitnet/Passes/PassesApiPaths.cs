using EvolutionaryArchitecture.Fitnet.Common.Api;

namespace EvolutionaryArchitecture.Fitnet.Passes;

internal static class PassesApiPaths
{
    internal const string Register = $"{ApiPaths.Root}/passes";
    internal const string MarkPassAsExpired = $"{ApiPaths.Root}/passes/{{id}}";
}