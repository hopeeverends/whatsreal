namespace WhatsReal.Domain.Interfaces;

using WhatsReal.Domain.Entities;

/// <summary>
/// Repository interface for Property entities.
/// Designed to work with both mock data and EF Core in future.
/// </summary>
public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync(Guid id);
    Task<List<Property>> GetAllAsync();
    Task<PagedResult<Property>> SearchAsync(PropertySearchCriteria criteria);
    Task<List<Property>> GetByAgentIdAsync(Guid agentId);
    Task<Property> CreateAsync(Property property);
    Task<Property> UpdateAsync(Property property);
    Task DeleteAsync(Guid id);
}

/// <summary>
/// Repository interface for Agent entities.
/// </summary>
public interface IAgentRepository
{
    Task<Agent?> GetByIdAsync(Guid id);
    Task<List<Agent>> GetAllAsync();
    Task<Agent> CreateAsync(Agent agent);
    Task<Agent> UpdateAsync(Agent agent);
    Task DeleteAsync(Guid id);
}

/// <summary>
/// Repository interface for PropertyInquiry entities.
/// </summary>
public interface IPropertyInquiryRepository
{
    Task<PropertyInquiry?> GetByIdAsync(Guid id);
    Task<List<PropertyInquiry>> GetByPropertyIdAsync(Guid propertyId);
    Task<PropertyInquiry> CreateAsync(PropertyInquiry inquiry);
    Task<PropertyInquiry> UpdateAsync(PropertyInquiry inquiry);
}

/// <summary>
/// Search criteria for properties.
/// </summary>
public class PropertySearchCriteria
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public PropertyType? Type { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public bool? IsFurnished { get; set; }
    public string? City { get; set; }
    
    public string? SortBy { get; set; }
    public string SortDirection { get; set; } = "asc";
    
    public string? SearchTerm { get; set; }
}

/// <summary>
/// Paged result container for repository queries.
/// </summary>
public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
