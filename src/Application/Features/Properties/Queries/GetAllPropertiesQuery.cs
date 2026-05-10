namespace WhatsReal.Application.Features.Properties.Queries;

using MediatR;
using WhatsReal.Domain.Interfaces;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Query to get all properties with pagination.
/// </summary>
public class GetAllPropertiesQuery : IRequest<PagedResponse<PropertyDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Handler for GetAllPropertiesQuery.
/// </summary>
public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, PagedResponse<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAgentRepository _agentRepository;

    public GetAllPropertiesQueryHandler(
        IPropertyRepository propertyRepository,
        IAgentRepository agentRepository)
    {
        _propertyRepository = propertyRepository;
        _agentRepository = agentRepository;
    }

    public async Task<PagedResponse<PropertyDto>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
    {
        var criteria = new PropertySearchCriteria
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await _propertyRepository.SearchAsync(criteria);
        
        var dtos = result.Items.Select(p => new PropertyDto
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

        return PagedResponse<PropertyDto>.Create(
            dtos,
            result.TotalCount,
            result.PageNumber,
            result.PageSize);
    }
}
