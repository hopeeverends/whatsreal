namespace WhatsReal.Application.Features.Agents.Queries;

using MediatR;
using WhatsReal.Domain.Interfaces;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Query to get all agents.
/// </summary>
public class GetAllAgentsQuery : IRequest<List<AgentListDto>>
{
}

/// <summary>
/// Handler for GetAllAgentsQuery.
/// </summary>
public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, List<AgentListDto>>
{
    private readonly IAgentRepository _agentRepository;

    public GetAllAgentsQueryHandler(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public async Task<List<AgentListDto>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
    {
        var agents = await _agentRepository.GetAllAsync();
        
        return agents.Select(a => new AgentListDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Email = a.Email,
            PhoneNumber = a.PhoneNumber,
            ImageUrl = a.ImageUrl,
            PropertyCount = a.Properties.Count
        }).ToList();
    }
}
