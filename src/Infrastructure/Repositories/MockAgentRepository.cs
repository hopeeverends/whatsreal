namespace WhatsReal.Infrastructure.Repositories;

using WhatsReal.Domain.Entities;
using WhatsReal.Domain.Interfaces;

/// <summary>
/// Mock in-memory repository for Agent entities.
/// Uses static data for Phase 1. Will be replaced with EF Core in Phase 2.
/// </summary>
public class MockAgentRepository : IAgentRepository
{
    private static List<Agent> _agents = [];

    /// <summary>
    /// Initializes the repository with seed data.
    /// </summary>
    public static void Initialize(List<Agent> seedData)
    {
        _agents = seedData;
    }

    public Task<Agent?> GetByIdAsync(Guid id)
    {
        var agent = _agents.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(agent);
    }

    public Task<List<Agent>> GetAllAsync()
    {
        return Task.FromResult(_agents.ToList());
    }

    public Task<Agent> CreateAsync(Agent agent)
    {
        _agents.Add(agent);
        return Task.FromResult(agent);
    }

    public Task<Agent> UpdateAsync(Agent agent)
    {
        var index = _agents.FindIndex(a => a.Id == agent.Id);
        if (index >= 0)
        {
            agent.UpdatedAt = DateTime.UtcNow;
            _agents[index] = agent;
        }
        return Task.FromResult(agent);
    }

    public Task DeleteAsync(Guid id)
    {
        _agents.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }
}
