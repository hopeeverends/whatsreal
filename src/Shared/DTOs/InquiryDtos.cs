namespace WhatsReal.Shared.DTOs;

/// <summary>
/// DTO for PropertyInquiry entity.
/// </summary>
public class PropertyInquiryDto
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// Request DTO for creating a property inquiry.
/// </summary>
public class CreatePropertyInquiryRequest
{
    public Guid PropertyId { get; set; }
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Message { get; set; } = null!;
}
