namespace EvolutionaryArchitecture.Fitnet.Contracts.Core;

using AttachAnnexToBindingContract.BusinessRules;
using Common.Core.BussinessRules;
using DomainDrivenDesign.BuildingBlocks;
using SignContract;
using TerminateBindingContract;
using TerminateBindingContract.BusinessRules;

public sealed class BindingContract : Entity
{
    public BindingContractId Id { get; init; }
    public ContractId ContractId { get; init; }
    public Guid CustomerId { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTimeOffset? TerminatedAt { get; set; }
    public DateTimeOffset BindingFrom { get; init; }
    public DateTimeOffset ExpiringAt { get; init; }
    public ICollection<Annex> AttachedAnnexes { get; }

    private BindingContract(
        ContractId contractId,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset expiringAt)
    {
        Id = BindingContractId.Create();
        ContractId = contractId;
        CustomerId = customerId;
        Duration = duration;
        ExpiringAt = expiringAt;
        BindingFrom = bindingFrom;
        AttachedAnnexes = [];

        var @event = BindingContractStartedEvent.Raise(BindingFrom, ExpiringAt);
        RecordEvent(@event);
    }

    internal static BindingContract Start(
        ContractId id,
        Guid customerId,
        TimeSpan duration,
        DateTimeOffset bindingFrom,
        DateTimeOffset expiringAt) => new(id, customerId, duration, bindingFrom, expiringAt);

    public ErrorOr<AnnexId> AttachAnnex(DateTimeOffset validFrom, DateTimeOffset now) => BusinessRuleValidator.Validate(
            new AnnexCanOnlyBeAttachedToActiveBindingContractRule(TerminatedAt, ExpiringAt, now),
            new AnnexCanOnlyStartDuringBindingContractPeriodRule(ExpiringAt, validFrom))
        .Then(_ =>
        {
            var annex = Annex.Attach(Id, validFrom);

            AttachedAnnexes.Add(annex);

            return annex.Id;
        });


    public ErrorOr<Success> Terminate(DateTimeOffset terminatedAt) => BusinessRuleValidator.Validate(
                new TerminationIsPossibleOnlyAfterThreeMonthsHavePassedRule(BindingFrom, terminatedAt))
            .Then(_ =>
            {
                TerminatedAt = terminatedAt;

                var @event = BindingContractTerminatedEvent.Raise(terminatedAt);
                RecordEvent(@event);

                return new Success();
            });
}

public record struct ContractId(Guid Value)
{
    internal static ContractId Create() => new(Guid.NewGuid());
}

public readonly record struct BindingContractId(Guid Value)
{
    internal static BindingContractId Create() => new(Guid.NewGuid());
}
