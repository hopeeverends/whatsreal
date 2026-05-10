namespace WhatsReal.Infrastructure.Repositories;

using WhatsReal.Domain.Entities;
using WhatsReal.Domain.Interfaces;

/// <summary>
/// Mock in-memory repository for PropertyInquiry entities.
/// Uses static data for Phase 1. Will be replaced with EF Core in Phase 2.
/// </summary>
public class MockPropertyInquiryRepository : IPropertyInquiryRepository
{
    private static List<PropertyInquiry> _inquiries = [];

    /// <summary>
    /// Initializes the repository with seed data.
    /// </summary>
    public static void Initialize(List<PropertyInquiry> seedData)
    {
        _inquiries = seedData;
    }

    public Task<PropertyInquiry?> GetByIdAsync(Guid id)
    {
        var inquiry = _inquiries.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(inquiry);
    }

    public Task<List<PropertyInquiry>> GetByPropertyIdAsync(Guid propertyId)
    {
        var inquiries = _inquiries
            .Where(i => i.PropertyId == propertyId)
            .ToList();

        return Task.FromResult(inquiries);
    }

    public Task<PropertyInquiry> CreateAsync(PropertyInquiry inquiry)
    {
        _inquiries.Add(inquiry);
        return Task.FromResult(inquiry);
    }

    public Task<PropertyInquiry> UpdateAsync(PropertyInquiry inquiry)
    {
        var index = _inquiries.FindIndex(i => i.Id == inquiry.Id);
        if (index >= 0)
        {
            inquiry.UpdatedAt = DateTime.UtcNow;
            _inquiries[index] = inquiry;
        }
        return Task.FromResult(inquiry);
    }
}
