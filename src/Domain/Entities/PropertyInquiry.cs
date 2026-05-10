namespace WhatsReal.Domain.Entities;

/// <summary>
/// Represents an inquiry about a property.
/// </summary>
public class PropertyInquiry
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Property? Property { get; set; }
    
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Message { get; set; } = null!;
    
    public InquiryStatus Status { get; set; } = InquiryStatus.New;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the PropertyInquiry class.
    /// </summary>
    public PropertyInquiry()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Enumeration of inquiry statuses.
/// </summary>
public enum InquiryStatus
{
    New,
    Read,
    Contacted,
    Archived
}
