namespace EvolutionaryArchitecture.Fitnet.Contracts.Application.Commands.Prepare.Exceptions;

using EvolutionaryArchitecture.Fitnet.Common.Core.BusinessRules;

internal sealed class CustomerHasNotSignedContractException : BusinessRuleValidationException
{
    internal CustomerHasNotSignedContractException(Guid customerId, Guid contractId, DateTimeOffset preparedAt) : 
        base($"Customer '{customerId}' has not signed contract '{contractId}' prepared: {preparedAt:F}. New contract cannot be prepared for this customer.")
    {
    }
}