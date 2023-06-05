namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Preapre.Exceptions;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class CustomerHasUnsignedContractException : BusinessRuleValidationException
{
    internal CustomerHasUnsignedContractException(Guid customerId, Guid contractId, DateTimeOffset preparedAt) : 
        base($"Customer '{customerId}' has unsigned contract '{contractId}' prepared at '{preparedAt:F}'. New contract cannot be prepared for this customer.")
    {
    }
}