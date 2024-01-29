namespace EvolutionaryArchitecture.Fitnet.Passes.Api.Common.EventBus.Outbox;

using DataAccess.Database;
using MassTransit;

internal static class OutboxExtensions
{
    internal static void ConfigureOutbox(this IBusRegistrationConfigurator configurator) => configurator.AddEntityFrameworkOutbox<PassesPersistence>(entityFrameworkOutboxConfigurator =>
    {
        entityFrameworkOutboxConfigurator.UsePostgres();
        entityFrameworkOutboxConfigurator.UseBusOutbox();
        entityFrameworkOutboxConfigurator.DuplicateDetectionWindow = TimeSpan.FromSeconds(30);
    });
}
