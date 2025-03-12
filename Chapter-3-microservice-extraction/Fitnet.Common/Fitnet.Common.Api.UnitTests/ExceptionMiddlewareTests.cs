namespace EvolutionaryArchitecture.Fitnet.Common.Api.UnitTests;

using ErrorHandling;
using Core.BusinessRules;
using System.IO;
using System.Threading.Tasks;
using System;

public sealed class ExceptionMiddlewareTests
{
    private readonly HttpContext _context = GetHttpContext();

    [Fact]
    internal async Task Given_business_rule_validation_exception_Then_returns_conflict()
    {
        // Arrange
        const string exceptionMessage = "Business rule not met";
        var middleware =
            new ExceptionMiddleware(context => throw new BusinessRuleValidationException(exceptionMessage));

        // Act
        await middleware.InvokeAsync(_context);

        // Assert
        _context.Response.StatusCode.ShouldBe((int)HttpStatusCode.Conflict);

        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.ShouldBe(exceptionMessage);
    }

    [Fact]
    internal async Task Given_other_than_business_rule_validation_exception_Then_returns_internal_server_error()
    {
        // Arrange
        const string exceptionMessage = "Some exception";
        var middleware =
            new ExceptionMiddleware(context => throw new InvalidOperationException(exceptionMessage));

        // Act
        await middleware.InvokeAsync(_context);

        // Assert
        _context.Response.StatusCode.ShouldBe((int)HttpStatusCode.InternalServerError);

        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.ShouldBe(exceptionMessage);
    }

    private static DefaultHttpContext GetHttpContext() =>
        new()
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };

    private async Task<string> GetExceptionResponseMessage()
    {
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var streamReader = new StreamReader(_context.Response.Body);
        var responseBody = await streamReader.ReadToEndAsync();
        var responseContent = JsonConvert.DeserializeObject<dynamic>(responseBody);

        return responseContent != null ? (string)responseContent.Message : string.Empty;
    }
}
