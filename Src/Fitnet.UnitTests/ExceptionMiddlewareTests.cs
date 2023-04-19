namespace SuperSimpleArchitecture.Fitnet.UnitTests;

using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SuperSimpleArchitecture.Fitnet.Shared.BusinessRulesEngine;

public class ExceptionMiddlewareTests
{
    private readonly HttpContext _context;
    
    public ExceptionMiddlewareTests()
    {
        _context = GetHttpContext();
    }
    
    [Fact]
    public async Task Given_business_rule_validation_exception_Then_returns_conflict()
    {
        // Arrange
        const string exceptionMessage = "Business rule not met";
        var middleware =
            new ExceptionMiddleware(context => throw new BusinessRuleValidationException(exceptionMessage));

        // Act
        await middleware.InvokeAsync(_context);

        // Assert
        _context.Response.StatusCode.Should().Be((int)HttpStatusCode.Conflict);
        
        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.Should().Be(exceptionMessage);
    }
    
    [Fact]
    public async Task Given_other_than_business_rule_validation_exception_Then_returns_internal_server_error()
    {
        // Arrange
        const string exceptionMessage = "Some exception";
        var middleware =
            new ExceptionMiddleware(context => throw new Exception(exceptionMessage));

        // Act
        await middleware.InvokeAsync(_context);

        // Assert
        _context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        
        var responseMessage = await GetExceptionResponseMessage();
        responseMessage.Should().Be(exceptionMessage);
    }
    
    private static HttpContext GetHttpContext()
    {
        return new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };
    }
    
    private async Task<string> GetExceptionResponseMessage()
    {
        _context.Response.Body.Seek(0, SeekOrigin.Begin);
        using var streamReader = new StreamReader(_context.Response.Body);
        var responseBody = await streamReader.ReadToEndAsync();
        var responseContent = JsonConvert.DeserializeObject<dynamic>(responseBody);
        
        return responseContent != null ? (string)responseContent.Message : string.Empty;
    }
}