namespace WhatsReal.Application.Features.Agents.Queries;

using MediatR;
using WhatsReal.Domain.Interfaces;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Query to get an agent by ID with their properties.
/// </summary>
public class GetAgentByIdQuery : IRequest<AgentDetailDto?>
{
    public Guid Id { get; set; }

    public GetAgentByIdQuery(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// Handler for GetAgentByIdQuery.
/// </summary>
public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, AgentDetailDto?>
{
    private readonly IAgentRepository _agentRepository;

    public GetAgentByIdQueryHandler(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public async Task<AgentDetailDto?> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var agent = await _agentRepository.GetByIdAsync(request.Id);
        if (agent == null)
            return null;

        var propertyDtos = agent.Properties.Select(p => new PropertyDto
        {
            Id = p.Id,
            Title = p.Title,
            Price = p.Price,
            Bedrooms = p.Bedrooms,
            Bathrooms = p.Bathrooms,
            City = p.City,
            ThumbnailUrl = p.ImageUrls.FirstOrDefault() ?? "https://via.placeholder.com/400x300?text=Property",
            IsFurnished = p.IsFurnished,
            PropertyType = p.Type.ToString()
        }).ToList();

        return new AgentDetailDto
        {
            Id = agent.Id,
            FirstName = agent.FirstName,
            LastName = agent.LastName,
            Email = agent.Email,
            PhoneNumber = agent.PhoneNumber,
            Bio = agent.Bio,
            ImageUrl = agent.ImageUrl,
            CreatedAt = agent.CreatedAt,
            Properties = propertyDtos
        };
    }
}
