using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Sample.Architecture.UnitTests;

public class ArchitectureTests
{
    private const string ApplicationNamespace = "Sample.Application";
    private const string DomainNamespace = "Sample.Domain";
    private const string InfrastructureNamespace = "Sample.Infrastructure";
    private const string PresentationNamespace = "Sample.Presentation";

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Domain.AssemblyReference.Assembly;

        string[] otherProjects = {
            ApplicationNamespace,
            InfrastructureNamespace,
            PresentationNamespace
        };

        // Act
        TestResult testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Application.AssemblyReference.Assembly;

        string[] otherProjects = {
            InfrastructureNamespace,
            PresentationNamespace
        };

        // Act
        TestResult testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
    
    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        Assembly assembly = Infrastructure.AssemblyReference.Assembly;

        string[] otherProjects = {
            PresentationNamespace
        };

        // Act
        TestResult testResult = Types.InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Handlers_Should_Have_DependencyOnDomain()
    {
        // Arrange
        Assembly assembly = Application.AssemblyReference.Assembly;

        // Act
        TestResult testResult = Types.InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }
}