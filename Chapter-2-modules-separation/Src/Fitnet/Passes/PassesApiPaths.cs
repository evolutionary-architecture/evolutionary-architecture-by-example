using EvolutionaryArchitecture.Fitnet.ReusableElements;

namespace EvolutionaryArchitecture.Fitnet.Passes;

using Shared;

internal static class PassesApiPaths
{
    internal const string Register = $"{ApiPaths.Root}/passes";
    internal const string MarkPassAsExpired = $"{ApiPaths.Root}/passes/{{id}}";
}