namespace EvolutionaryArchitecture.Fitnet.Contracts.Api.SignContract;

using EvolutionaryArchitecture.Fitnet.Common.Api.Validations;
using Application;
using Common.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class SignContractEndpoint
{
    internal static void MapSignContract(this IEndpointRouteBuilder app) => app.MapPatch(ContractsApiPaths.Sign, async (
                Guid id,
                SignContractRequest request,
                IContractsModule contractsModule, CancellationToken cancellationToken) =>
            await contractsModule
                .ExecuteCommandAsync(request.ToCommand(id), cancellationToken)
                .Match(
                    bindingContractId => Results.Created($"/{ContractsApiPaths.BindingContracts}/{bindingContractId}",
                        bindingContractId), errors => errors.ToProblem()))
        .ValidateRequest<SignContractRequestValidator>()
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Signs prepared contract",
            Description =
                "This endpoint is used to sign prepared contract by customer.",
        })
        .Produces(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status409Conflict)
        .Produces(StatusCodes.Status500InternalServerError);
}
