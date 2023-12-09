namespace EvolutionaryArchitecture.Fitnet.UnitTests;

using EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;
using Common.ErrorHandling;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public sealed class GlobalExceptionHandlerTests
{
    private readonly HttpContext _context = GetHttpContext();
    private readonly ILogger<GlobalExceptionHandler> _logger = Substitute.For<ILogger<GlobalExceptionHandler>>();

    [Fact]
    internal async Task Given_business_rule_validation_exception_Then_returns_conflict()
    {
        // Arrange
        const string exceptionMessage = "Business rule not met";
        var exceptionHandler =
            new GlobalExceptionHandler(_logger);

        // Act
        await exceptionHandler.TryHandleAsync(_context, new BusinessRuleValidationException(exceptionMessage), default);

        // Assert
        _context.Response.StatusCode.Should().Be((int)HttpStatusCode.Conflict);

        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.Title.Should().Be(exceptionMessage);
    }

    [Fact]
    internal async Task Given_other_than_business_rule_validation_exception_Then_returns_internal_server_error()
    {
        // Arrange
        const string exceptionMessage = "Server Error";
        var exceptionHandler =
            new GlobalExceptionHandler(_logger);

        // Act
        await exceptionHandler.TryHandleAsync(_context, new InvalidCastException("test"), CancellationToken.None);

        // Assert
        _context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);

        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.Title.Should().Be(exceptionMessage);
    }

    private static DefaultHttpContext GetHttpContext() =>
        new()
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };

    private async Task<ProblemDetails> GetExceptionResponseMessage()
    {
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var streamReader = new StreamReader(_context.Response.Body);
        var responseBody = await streamReader.ReadToEndAsync();
        var problemDetails = JsonConvert.DeserializeObject<ProblemDetails>(responseBody);
        return problemDetails!;
    }
}
