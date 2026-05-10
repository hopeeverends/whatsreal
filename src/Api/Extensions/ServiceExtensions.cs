namespace WhatsReal.Api.Extensions;

using MediatR;
using FluentValidation;
using WhatsReal.Application;
using WhatsReal.Infrastructure;
using WhatsReal.Infrastructure.Repositories;
using WhatsReal.Infrastructure.Seeders;
using WhatsReal.Domain.Interfaces;

/// <summary>
/// Dependency injection extension methods.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds domain layer services.
    /// </summary>
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    /// Adds application layer services (MediatR, AutoMapper, FluentValidation).
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(WhatsReal.Application.AssemblyReference).Assembly);
        });

        services.AddAutoMapper(_ => { }, typeof(WhatsReal.Application.AssemblyReference).Assembly);
        
        services.AddValidatorsFromAssembly(typeof(WhatsReal.Application.AssemblyReference).Assembly);

        return services;
    }

    /// <summary>
    /// Adds infrastructure layer services (repositories, data access).
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Initialize seed data
        var agents = DataSeeder.GetAgents();
        var properties = DataSeeder.GetProperties();

        // Link agents to properties
        foreach (var property in properties)
        {
            var agent = agents.FirstOrDefault(a => a.Id == property.AgentId);
            if (agent != null)
            {
                agent.Properties.Add(property);
            }
        }

        // Register mock repositories as singletons (data persists during app lifetime)
        MockPropertyRepository.Initialize(properties);
        MockAgentRepository.Initialize(agents);
        MockPropertyInquiryRepository.Initialize(new());

        services.AddSingleton<IPropertyRepository, MockPropertyRepository>();
        services.AddSingleton<IAgentRepository, MockAgentRepository>();
        services.AddSingleton<IPropertyInquiryRepository, MockPropertyInquiryRepository>();

        return services;
    }

}
