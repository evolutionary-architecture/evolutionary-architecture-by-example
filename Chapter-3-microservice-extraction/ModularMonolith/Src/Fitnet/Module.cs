namespace EvolutionaryArchitecture.Fitnet;

internal record Module(string Value)
{
    internal static readonly Module Offers = new("Offers");
    internal static readonly Module Passes = new("Passes");
    internal static readonly Module Reports = new("Reports");

    public static implicit operator string(Module module) => module.Value;
}