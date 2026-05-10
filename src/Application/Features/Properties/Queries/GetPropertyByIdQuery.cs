namespace WhatsReal.Application.Features.Properties.Queries;

using MediatR;
using WhatsReal.Domain.Entities;
using WhatsReal.Domain.Interfaces;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Query to get a single property by ID.
/// </summary>
public class GetPropertyByIdQuery : IRequest<PropertyDetailDto?>
{
    public Guid Id { get; set; }

    public GetPropertyByIdQuery(Guid id)
    {
        Id = id;
    }
}

/// <summary>
/// Handler for GetPropertyByIdQuery.
/// </summary>
public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, PropertyDetailDto?>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAgentRepository _agentRepository;

    public GetPropertyByIdQueryHandler(
        IPropertyRepository propertyRepository,
        IAgentRepository agentRepository)
    {
        _propertyRepository = propertyRepository;
        _agentRepository = agentRepository;
    }

    public async Task<PropertyDetailDto?> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
    {
        var property = await _propertyRepository.GetByIdAsync(request.Id);
        if (property == null)
            return null;

        Agent? agent = null;
        if (property.AgentId != Guid.Empty)
        {
            agent = await _agentRepository.GetByIdAsync(property.AgentId);
        }

        var agentDto = agent != null ? new AgentListDto
        {
            Id = agent.Id,
            FirstName = agent.FirstName,
            LastName = agent.LastName,
            Email = agent.Email,
            PhoneNumber = agent.PhoneNumber,
            ImageUrl = agent.ImageUrl,
            PropertyCount = agent.Properties.Count
        } : null;

        return new PropertyDetailDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Price = property.Price,
            PropertyType = property.Type.ToString(),
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            IsFurnished = property.IsFurnished,
            SquareFeet = property.SquareFeet,
            Address = property.Address,
            City = property.City,
            State = property.State,
            ZipCode = property.ZipCode,
            Latitude = property.Latitude,
            Longitude = property.Longitude,
            ImageUrls = property.ImageUrls,
            IsAvailable = property.IsAvailable,
            Agent = agentDto,
            CreatedAt = property.CreatedAt,
            UpdatedAt = property.UpdatedAt
        };
    }
}
