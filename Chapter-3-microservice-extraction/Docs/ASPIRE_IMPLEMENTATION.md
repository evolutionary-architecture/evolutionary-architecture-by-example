# .NET Aspire Implementation in Chapter 3

This document describes the .NET Aspire implementation added to Chapter 3 of the Evolutionary Architecture by Example project.

## Overview

.NET Aspire is a new cloud-ready stack for building observable, production-ready, distributed applications. This implementation adds Aspire to Chapter 3, providing:

- **Service Orchestration**: Automatic startup and configuration of all services
- **Observability**: Built-in distributed tracing, metrics, and structured logging
- **Service Discovery**: Automatic service-to-service communication
- **Health Checks**: Monitoring of service and dependency health
- **Resilience**: Automatic retry policies and circuit breakers

## Components Added

### 1. Fitnet.ServiceDefaults Project

A shared library that provides common Aspire configurations for all services.

**Location**: `Chapter-3-microservice-extraction/Fitnet.ServiceDefaults/`

**Key Features**:
- OpenTelemetry configuration for logs, metrics, and traces
- Service discovery client
- HTTP resilience handlers (retries, circuit breakers)
- Health check endpoints (`/health` and `/alive`)
- OTLP exporter for telemetry data

**Usage**:
```csharp
var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var app = builder.Build();
app.MapDefaultEndpoints();
```

### 2. Fitnet.AppHost Project

The orchestration layer that manages all services and resources.

**Location**: `Chapter-3-microservice-extraction/Fitnet.AppHost/`

**Managed Resources**:
- PostgreSQL database (`postgres` with `fitnet` database)
- RabbitMQ message broker (with management plugin)
- Fitnet modular monolith (on port 8080)
- Fitnet.Contracts microservice (on port 8081)

**Configuration**:
```csharp
var postgres = builder.AddPostgres("postgres")
    .WithImage("postgres")
    .WithImageTag("14.3")
    .WithHealthCheck();

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin()
    .WithHealthCheck();
```

## Changes to Existing Code

### Connection String Support

All database and messaging modules have been updated to support both Aspire connection strings and legacy configuration:

#### EventBusModule (RabbitMQ)
**Files Modified**:
- `Fitnet/Src/Passes/Fitnet.Passes.Api/Common/EventBus/EventBusModule.cs`
- `Fitnet.Contracts/Src/Fitnet.Contracts.Infrastructure/EventBus/EventBusModule.cs`

**Pattern**:
```csharp
// Try Aspire connection string first
var connectionString = configuration.GetConnectionString("rabbitmq");

if (!string.IsNullOrEmpty(connectionString))
{
    factoryConfigurator.Host(new Uri(connectionString));
}
else
{
    // Fallback to legacy configuration
    var options = context.GetRequiredService<IOptions<EventBusOptions>>();
    factoryConfigurator.Host(options.Value.Uri, hostConfigurator => { ... });
}
```

#### DatabaseModule (PostgreSQL)
**Files Modified**:
- `Fitnet/Src/Passes/Fitnet.Passes.DataAccess/Database/DatabaseModule.cs`
- `Fitnet/Src/Offers/Fitnet.Offers.DataAccess/Database/DatabaseModule.cs`
- `Fitnet.Contracts/Src/Fitnet.Contracts.Infrastructure/Database/DatabaseModule.cs`
- `Fitnet/Src/Reports/Fitnet.Reports/DataAccess/DatabaseConnectionFactory.cs`

**Pattern**:
```csharp
// Try Aspire connection string first
var connectionString = configuration.GetConnectionString("fitnet");

if (string.IsNullOrEmpty(connectionString))
{
    // Fallback to legacy configuration
    var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>();
    connectionString = databaseOptions.Value.ConnectionString;
}

options.UseNpgsql(connectionString);
```

### Program.cs Updates

**Files Modified**:
- `Fitnet/Src/Fitnet/Program.cs`
- `Fitnet.Contracts/Src/Fitnet.Contracts/Program.cs`

**Changes**:
```csharp
// Added Aspire service defaults
builder.AddServiceDefaults();

// Added default health check endpoints
app.MapDefaultEndpoints();
```

### Project References

Both main projects now reference `Fitnet.ServiceDefaults`:
- `Fitnet/Src/Fitnet/Fitnet.csproj`
- `Fitnet.Contracts/Src/Fitnet.Contracts/Fitnet.Contracts.csproj`

## How to Run with Aspire

### Prerequisites
- .NET SDK 9.0 or higher
- Docker (for containers)
- GitHub Personal Access Token with `read:packages` scope

### Running the Application

1. Configure NuGet credentials for GitHub Packages
2. Navigate to the AppHost directory:
   ```bash
   cd Chapter-3-microservice-extraction/Fitnet.AppHost
   ```
3. Run the AppHost:
   ```bash
   dotnet run
   ```

The Aspire Dashboard will open automatically in your browser.

### Accessing Services

- **Fitnet Modular Monolith**: http://localhost:8080
- **Contracts Microservice**: http://localhost:8081
- **Swagger UI (Fitnet)**: http://localhost:8080/swagger
- **Swagger UI (Contracts)**: http://localhost:8081/swagger
- **Aspire Dashboard**: URL shown in console output

### Aspire Dashboard Features

The dashboard provides:
- **Resources Tab**: View all containers and services
- **Console Logs**: Real-time logs from all services
- **Structured Logs**: Queryable structured logs
- **Traces**: Distributed tracing across services
- **Metrics**: Performance metrics and charts

## Benefits of Aspire Implementation

### For Development
- **Simplified Setup**: No need to manually run `docker-compose`
- **Integrated Dashboard**: Single place to monitor all services
- **Better Debugging**: Distributed tracing shows request flows
- **Quick Iteration**: Fast service restarts

### For Production Readiness
- **Observability**: Built-in telemetry collection
- **Resilience**: Automatic retry and circuit breaker policies
- **Health Monitoring**: Continuous health checks
- **Service Discovery**: Dynamic service location

### Backward Compatibility
- **Legacy Support**: Existing `docker-compose.yml` still works
- **Configuration Fallback**: Services work without Aspire
- **Minimal Changes**: Existing code patterns preserved

## Architecture Improvements

### Before Aspire
```
Developer → docker-compose → PostgreSQL + RabbitMQ
                          → Fitnet API
                          → Contracts API
```

### With Aspire
```
Developer → Aspire AppHost → PostgreSQL (container)
                           → RabbitMQ (container)
                           → Fitnet API (with ServiceDefaults)
                           → Contracts API (with ServiceDefaults)
                           → Aspire Dashboard
```

## Key Design Decisions

### 1. Backward Compatibility
All changes support both Aspire and legacy configurations. Services can run:
- With Aspire (recommended for local development)
- With docker-compose (still supported)
- Standalone (with manual infrastructure setup)

### 2. Connection String Priority
The implementation checks connection strings in this order:
1. Aspire-provided connection string (`GetConnectionString()`)
2. Legacy configuration (`EventBusOptions`, `DatabaseOptions`)

This ensures smooth migration and flexibility.

### 3. Minimal Code Changes
The implementation adds Aspire capabilities without significant refactoring:
- Two new projects (AppHost, ServiceDefaults)
- Minor updates to existing modules
- Preserved existing architecture patterns

### 4. PostgreSQL and RabbitMQ Versions
The AppHost uses the same versions as docker-compose:
- PostgreSQL: `postgres:14.3`
- RabbitMQ: `rabbitmq:management`

This ensures consistency across deployment methods.

## Testing Considerations

### Integration Tests
Integration tests continue to work unchanged because:
- They use Testcontainers for dependencies
- Aspire ServiceDefaults don't interfere with test infrastructure
- Configuration fallback ensures compatibility

### Local Development
Developers can choose their preferred approach:
- Aspire for full observability
- docker-compose for simpler setup
- Mix and match as needed

## Future Enhancements

Potential improvements for future iterations:

1. **Deployment Profiles**: Different configurations for dev/staging/prod
2. **Additional Integrations**: Redis, Azure Service Bus alternatives
3. **Custom Metrics**: Business-specific metrics in dashboard
4. **Performance Testing**: Load testing with Aspire infrastructure
5. **Cloud Deployment**: Azure Container Apps integration

## Summary

This Aspire implementation enhances Chapter 3 with modern cloud-native capabilities while maintaining full backward compatibility. It provides developers with powerful observability and orchestration tools without requiring changes to the core business logic or existing deployment methods.
