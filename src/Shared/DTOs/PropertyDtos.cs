namespace WhatsReal.Shared.DTOs;

/// <summary>
/// DTO for Property entity in list views.
/// </summary>
public class PropertyDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public string City { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public bool IsFurnished { get; set; }
    public string PropertyType { get; set; } = null!;
}

/// <summary>
/// DTO for Property entity in detail views.
/// </summary>
public class PropertyDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string PropertyType { get; set; } = null!;
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public bool IsFurnished { get; set; }
    public decimal SquareFeet { get; set; }
    
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public List<string> ImageUrls { get; set; } = [];
    public bool IsAvailable { get; set; }
    
    public AgentListDto? Agent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

/// <summary>
/// Request DTO for creating a property.
/// </summary>
public class CreatePropertyRequest
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string PropertyType { get; set; } = null!;
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public bool IsFurnished { get; set; }
    public decimal SquareFeet { get; set; }
    
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string State { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    public List<string> ImageUrls { get; set; } = [];
    public Guid AgentId { get; set; }
}
