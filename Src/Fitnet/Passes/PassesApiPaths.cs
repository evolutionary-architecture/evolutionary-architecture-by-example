namespace SuperSimpleArchitecture.Fitnet.Passes;

public static class PassesApiPaths
{
    public const string Register = $"{ApiPaths.Root}/passes";
    public const string MarkPassAsExpired = $"{ApiPaths.Root}/passes/{{id}}";
}