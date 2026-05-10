namespace WhatsReal.Application.Features.Properties.Queries;

using MediatR;
using WhatsReal.Domain.Interfaces;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Query to search properties with filters.
/// </summary>
public class SearchPropertiesQuery : IRequest<PagedResponse<PropertyDto>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? PropertyType { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public bool? IsFurnished { get; set; }
    public string? City { get; set; }
    
    public string? SortBy { get; set; }
    public string SortDirection { get; set; } = "asc";
    public string? SearchTerm { get; set; }
}

/// <summary>
/// Handler for SearchPropertiesQuery.
/// </summary>
public class SearchPropertiesQueryHandler : IRequestHandler<SearchPropertiesQuery, PagedResponse<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;

    public SearchPropertiesQueryHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<PagedResponse<PropertyDto>> Handle(SearchPropertiesQuery request, CancellationToken cancellationToken)
    {
        // Convert string property type to enum if provided
        Domain.Entities.PropertyType? propertyType = null;
        if (!string.IsNullOrEmpty(request.PropertyType) && 
            Enum.TryParse<Domain.Entities.PropertyType>(request.PropertyType, true, out var type))
        {
            propertyType = type;
        }

        var criteria = new PropertySearchCriteria
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            Type = propertyType,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            IsFurnished = request.IsFurnished,
            City = request.City,
            SortBy = request.SortBy,
            SortDirection = request.SortDirection,
            SearchTerm = request.SearchTerm
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
