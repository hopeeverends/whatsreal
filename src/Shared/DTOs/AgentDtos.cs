namespace WhatsReal.Shared.DTOs;

/// <summary>
/// DTO for Agent entity in list views.
/// </summary>
public class AgentListDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public int PropertyCount { get; set; }
}

/// <summary>
/// DTO for Agent entity in detail views.
/// </summary>
public class AgentDetailDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PropertyDto> Properties { get; set; } = [];
}

/// <summary>
/// Request DTO for creating an agent.
/// </summary>
public class CreateAgentRequest
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Bio { get; set; }
    public string? ImageUrl { get; set; }
}
