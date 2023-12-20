﻿namespace EvolutionaryArchitecture.Fitnet.ArchitectureTests.Contracts;

using System.Reflection;
using Common.Predicates;
using NetArchTest.Rules;

public class ContractsModuleArchitectureTests
{
    private const string Contracts = "EvolutionaryArchitecture.Fitnet.Contracts";
    private const string Event = "Event";
    private readonly Assembly _solution = typeof(Program).Assembly;

    [Fact]
    internal void Modules_Should_Only_Communicate_With_Contracts_Module_Via_Events()
    {
        // Arrange
        var contractsModule = Types.InAssembly(_solution)
            .That()
            .ResideInNamespace(Contracts)
            .And()
            .DoNotHaveNameEndingWith(Event);
        var contractsModuleTypes = contractsModule.GetModuleTypes();

        var othersModules = Types.InAssembly(_solution)
            .That()
            .DoNotResideInNamespace(Contracts)
            .And()
            .DoNotHaveName(nameof(Program));

        // Act
        var rules = othersModules
                                            .Should()
                                            .NotHaveDependencyOnAny(contractsModuleTypes);
        var validationResult = rules!.GetResult();

        // Assert
        validationResult.FailingTypes.Should()!.BeNull();
    }
}
