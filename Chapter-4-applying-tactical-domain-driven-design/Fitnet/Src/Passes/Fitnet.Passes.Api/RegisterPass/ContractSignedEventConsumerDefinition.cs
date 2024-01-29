namespace EvolutionaryArchitecture.Fitnet.Passes.Api.RegisterPass;

using DataAccess.Database;
using MassTransit;
using static TimeSpan;

public sealed class ContractSignedEventConsumerDefinition : ConsumerDefinition<ContractSignedEventConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ContractSignedEventConsumer> consumerConfigurator, IRegistrationContext context)
    {
        endpointConfigurator.UseMessageRetry(retryConfigurator => retryConfigurator.Interval(
            3,
            FromSeconds(1)));

        endpointConfigurator.UseScheduledRedelivery(retryConfigurator => retryConfigurator.Intervals(
            FromMinutes(5),
            FromHours(3),
            FromHours(8),
            FromDays(1)));
        endpointConfigurator.UseEntityFrameworkOutbox<PassesPersistence>(context);
        base.ConfigureConsumer(endpointConfigurator, consumerConfigurator, context);
    }
}
