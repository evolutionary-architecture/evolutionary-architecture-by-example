namespace SuperSimpleArchitecture.Fitnet;

public static class ApiPaths
{
    private const string Api = "api";

    public static class Passes
    {
        public const string Register = $"{Api}/passes";
        public const string MarkPassAsExpired = $"{Api}/passes/{{id}}/mark-as-expired";
    }
    
    public const string Contracts = $"{Api}/contracts";
}