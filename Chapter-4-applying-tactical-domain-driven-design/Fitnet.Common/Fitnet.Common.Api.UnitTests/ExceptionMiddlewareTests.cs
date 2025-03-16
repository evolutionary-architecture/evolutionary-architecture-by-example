namespace EvolutionaryArchitecture.Fitnet.Common.Api.UnitTests;

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ErrorHandling;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

public sealed class ExceptionMiddlewareTests
{
    private readonly HttpContext _context = GetHttpContext();

    [Fact]
    internal async Task Given_any_unexpected_exception_Then_returns_internal_server_error()
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
