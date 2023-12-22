namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests.Common;

internal static class Solution
{
    private static readonly Assembly Program = typeof(Program).Assembly;
    internal static Types Types => Types.InAssembly(Program);
}
