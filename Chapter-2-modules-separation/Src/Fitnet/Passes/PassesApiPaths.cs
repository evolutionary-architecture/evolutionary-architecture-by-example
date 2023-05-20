namespace EvolutionaryArchitecture.Fitnet.Passes;

using ReusableElements;

internal static class PassesApiPaths
{
    internal const string Register = $"{ApiPaths.Root}/passes";
    internal const string MarkPassAsExpired = $"{ApiPaths.Root}/passes/{{id}}";
}