namespace WhatsReal.Infrastructure.Repositories;

using WhatsReal.Domain.Entities;
using WhatsReal.Domain.Interfaces;

/// <summary>
/// Mock in-memory repository for Property entities.
/// Uses static data for Phase 1. Will be replaced with EF Core in Phase 2.
/// </summary>
public class MockPropertyRepository : IPropertyRepository
{
    private static List<Property> _properties = [];

    /// <summary>
    /// Initializes the repository with seed data.
    /// </summary>
    public static void Initialize(List<Property> seedData)
    {
        _properties = seedData;
    }

    public Task<Property?> GetByIdAsync(Guid id)
    {
        var property = _properties.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(property);
    }

    public Task<List<Property>> GetAllAsync()
    {
        return Task.FromResult(_properties.ToList());
    }

    public Task<PagedResult<Property>> SearchAsync(PropertySearchCriteria criteria)
    {
        var query = _properties.AsEnumerable();

        // Apply filters
        if (criteria.MinPrice.HasValue)
            query = query.Where(p => p.Price >= criteria.MinPrice.Value);

        if (criteria.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= criteria.MaxPrice.Value);

        if (criteria.Type.HasValue)
            query = query.Where(p => p.Type == criteria.Type.Value);

        if (criteria.Bedrooms.HasValue)
            query = query.Where(p => p.Bedrooms >= criteria.Bedrooms.Value);

        if (criteria.Bathrooms.HasValue)
            query = query.Where(p => p.Bathrooms >= criteria.Bathrooms.Value);

        if (criteria.IsFurnished.HasValue)
            query = query.Where(p => p.IsFurnished == criteria.IsFurnished.Value);

        if (!string.IsNullOrWhiteSpace(criteria.City))
            query = query.Where(p => p.City.Equals(criteria.City, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(criteria.SearchTerm))
        {
            var searchTerm = criteria.SearchTerm.ToLower();
            query = query.Where(p =>
                p.Title.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm) ||
                p.City.ToLower().Contains(searchTerm));
        }

        // Apply sorting
        query = criteria.SortBy?.ToLower() switch
        {
            "price" => criteria.SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price),
            "bedrooms" => criteria.SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? query.OrderByDescending(p => p.Bedrooms)
                : query.OrderBy(p => p.Bedrooms),
            "date" => criteria.SortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase)
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt),
            _ => query.OrderByDescending(p => p.CreatedAt)
        };

        // Apply pagination
        var totalCount = query.Count();
        var items = query
            .Skip((criteria.PageNumber - 1) * criteria.PageSize)
            .Take(criteria.PageSize)
            .ToList();

        return Task.FromResult(new PagedResult<Property>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = criteria.PageNumber,
            PageSize = criteria.PageSize
        });
    }

    public Task<List<Property>> GetByAgentIdAsync(Guid agentId)
    {
        var properties = _properties
            .Where(p => p.AgentId == agentId)
            .ToList();

        return Task.FromResult(properties);
    }

    public Task<Property> CreateAsync(Property property)
    {
        _properties.Add(property);
        return Task.FromResult(property);
    }

    public Task<Property> UpdateAsync(Property property)
    {
        var index = _properties.FindIndex(p => p.Id == property.Id);
        if (index >= 0)
        {
            property.UpdatedAt = DateTime.UtcNow;
            _properties[index] = property;
        }
        return Task.FromResult(property);
    }

    public Task DeleteAsync(Guid id)
    {
        _properties.RemoveAll(p => p.Id == id);
        return Task.CompletedTask;
    }
}
