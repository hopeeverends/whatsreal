namespace WhatsReal.Domain.Entities;

/// <summary>
/// Represents a real estate property.
/// </summary>
public class Property
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public PropertyType Type { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public bool IsFurnished { get; set; }
    public decimal SquareFeet { get; set; }
    
    // Location
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    // Images
    public List<string> ImageUrls { get; set; } = [];

    // Agent
    public Guid AgentId { get; set; }
    public Agent? Agent { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Initializes a new instance of the Property class.
    /// </summary>
    public Property()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Enumeration of property types.
/// </summary>
public enum PropertyType
{
    Apartment,
    House,
    Townhouse,
    Condo,
    Studio,
    Loft,
    Villa,
    Penthouse
}
