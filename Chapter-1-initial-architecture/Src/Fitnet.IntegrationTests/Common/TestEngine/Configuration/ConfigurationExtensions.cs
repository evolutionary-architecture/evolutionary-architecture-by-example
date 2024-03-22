namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.Configuration;

using System.Reflection;
using Events.EventBus.InMemory;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;
using EvolutionaryArchitecture.Fitnet.Common.Events.EventBus.InMemory;

internal static class ConfigurationExtensions
{
    internal static WebApplicationFactory<T> WithContainerDatabaseConfigured<T>(this WebApplicationFactory<T> webApplicationFactory, string connectionString)
        where T : class
    {
        var connectionStringsConfiguration = new Dictionary<string, string?>
        {
            {ConfigurationKeys.ContractsConnectionString, connectionString},
            {ConfigurationKeys.OffersConnectionString, connectionString},
            {ConfigurationKeys.PassesConnectionString, connectionString},
            {ConfigurationKeys.ReportsConnectionString, connectionString}
        };

        return webApplicationFactory.UseSettings(connectionStringsConfiguration);
    }

    private static WebApplicationFactory<T> UseSettings<T>(this WebApplicationFactory<T> webApplicationFactory, Dictionary<string, string?> settings)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder =>
        {
            foreach (var setting in settings)
            {
                webHostBuilder.UseSetting(setting.Key, setting.Value);
            }
        });

    internal static WebApplicationFactory<T> SetFakeSystemClock<T>(this WebApplicationFactory<T> webApplicationFactory, DateTimeOffset fakeDateTimeOffset)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
            services.AddSingleton<TimeProvider>(new FakeTimeProvider(fakeDateTimeOffset))));

    internal static WebApplicationFactory<T> WithFakeEventBus<T>(this WebApplicationFactory<T> webApplicationFactory, IEventBus eventBusMock)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
        {
            AddNotificationDecorator(eventBusMock, services, true);
        }));

    internal static WebApplicationFactory<T> WithoutEventHandlers<T>(this WebApplicationFactory<T> webApplicationFactory, IEventBus eventBusMock)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
        {
            AddNotificationDecorator(eventBusMock, services, false);
        }));

    internal static WebApplicationFactory<T> WithFakeConsumers<T>(this WebApplicationFactory<T> webApplicationFactory)
        where T : class =>
        webApplicationFactory.WithWebHostBuilder(webHostBuilder => webHostBuilder.ConfigureTestServices(services =>
        {
            services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
            var decorator = services.FirstOrDefault(s => s.ImplementationType == typeof(NotificationDecorator<>));
            if (decorator is not null)
            {
                services.Remove(decorator);
            }
        }));

    private static void AddNotificationDecorator(IEventBus eventBusMock, IServiceCollection services, bool hasSingleEventHandling)
    {
        var notificationHandlerServices = services.Where(s =>
                s.ServiceType.IsGenericType &&
                s.ServiceType.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
            .ToList();

        foreach (var notificationHandlerService in notificationHandlerServices)
        {
            var serviceType = notificationHandlerService.ServiceType;
            var implementationType = notificationHandlerService.ImplementationType;

            services.AddTransient(serviceType, serviceProvider =>
            {
                var notificationHandler = hasSingleEventHandling
                    ? NotificationHandlerWithFakeEventBus(eventBusMock, implementationType, serviceProvider)
                    : null;

                var decoratorType = typeof(NotificationDecorator<>).MakeGenericType(serviceType.GenericTypeArguments[0]);
                return Activator.CreateInstance(decoratorType, eventBusMock, notificationHandler)!;
            });
        }
    }

    private static object NotificationHandlerWithFakeEventBus(IEventBus eventBusMock, Type? implementationType,
        IServiceProvider serviceProvider)
    {
        var constructor = implementationType!.GetConstructors().FirstOrDefault()!;
        var parameters = constructor.GetParameters();
        var handlerDependencies = new object[parameters.Length];
        for (var i = 0; i < parameters.Length; i++)
        {
            if (parameters[i].ParameterType == typeof(IEventBus))
            {
                handlerDependencies[i] = eventBusMock;
                continue;
            }
            handlerDependencies[i] = serviceProvider.GetService(parameters[i]!.ParameterType)!;
        }

        var notificationHandler = Activator.CreateInstance(implementationType, handlerDependencies)!;
        return notificationHandler;
    }
}
