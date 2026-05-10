namespace WhatsReal.Application.Features.Inquiries.Commands;

using MediatR;
using FluentValidation;
using WhatsReal.Domain.Interfaces;
using WhatsReal.Domain.Entities;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Command to create a new property inquiry.
/// </summary>
public class CreatePropertyInquiryCommand : IRequest<PropertyInquiryDto>
{
    public Guid PropertyId { get; set; }
    public string ContactName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Message { get; set; } = null!;
}

/// <summary>
/// Validator for CreatePropertyInquiryCommand.
/// </summary>
public class CreatePropertyInquiryCommandValidator : AbstractValidator<CreatePropertyInquiryCommand>
{
    public CreatePropertyInquiryCommandValidator()
    {
        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Property ID is required.");

        RuleFor(x => x.ContactName)
            .NotEmpty().WithMessage("Contact name is required.")
            .MaximumLength(100).WithMessage("Contact name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.")
            .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number format is invalid.");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required.")
            .Length(10, 1000).WithMessage("Message must be between 10 and 1000 characters.");
    }
}

/// <summary>
/// Handler for CreatePropertyInquiryCommand.
/// </summary>
public class CreatePropertyInquiryCommandHandler : IRequestHandler<CreatePropertyInquiryCommand, PropertyInquiryDto>
{
    private readonly IPropertyInquiryRepository _inquiryRepository;
    private readonly IPropertyRepository _propertyRepository;

    public CreatePropertyInquiryCommandHandler(
        IPropertyInquiryRepository inquiryRepository,
        IPropertyRepository propertyRepository)
    {
        _inquiryRepository = inquiryRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<PropertyInquiryDto> Handle(CreatePropertyInquiryCommand request, CancellationToken cancellationToken)
    {
        // Verify property exists
        var property = await _propertyRepository.GetByIdAsync(request.PropertyId);
        if (property == null)
            throw new ArgumentException($"Property with ID {request.PropertyId} not found.");

        var inquiry = new PropertyInquiry
        {
            PropertyId = request.PropertyId,
            ContactName = request.ContactName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Message = request.Message,
            Status = InquiryStatus.New
        };

        var createdInquiry = await _inquiryRepository.CreateAsync(inquiry);

        return new PropertyInquiryDto
        {
            Id = createdInquiry.Id,
            PropertyId = createdInquiry.PropertyId,
            ContactName = createdInquiry.ContactName,
            Email = createdInquiry.Email,
            PhoneNumber = createdInquiry.PhoneNumber,
            Message = createdInquiry.Message,
            Status = createdInquiry.Status.ToString(),
            CreatedAt = createdInquiry.CreatedAt
        };
    }
}
