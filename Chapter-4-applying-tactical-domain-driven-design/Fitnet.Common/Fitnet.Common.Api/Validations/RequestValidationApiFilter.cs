namespace EvolutionaryArchitecture.Fitnet.Common.Api.Validations;

using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

internal sealed class RequestValidationApiFilter<TRequestToValidate> : IEndpointFilter where TRequestToValidate : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var requestToValidate = context.Arguments.FirstOrDefault(argument => argument?.GetType() == typeof(TRequestToValidate)) as TRequestToValidate;
        var validator = context.HttpContext.RequestServices.GetService<IValidator<TRequestToValidate>>();

        if (validator is null)
        {
            return await next.Invoke(context);
        }

        var validationResult = await validator.ValidateAsync(requestToValidate!);
        if (validationResult.IsValid)
        {
            return await next.Invoke(context);
        }

        var errors = validationResult.ToDictionary();
        return Results.ValidationProblem(errors,
            statusCode: (int)HttpStatusCode.BadRequest);
    }
}
