namespace WhatsReal.Api.Endpoints;

using MediatR;
using FluentValidation;
using WhatsReal.Application.Features.Inquiries.Commands;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Inquiry endpoints for creating and managing property inquiries.
/// </summary>
public static class InquiryEndpoints
{
    public static void MapInquiryEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/inquiries")
            .WithName("Inquiries")
            .WithTags("Inquiries")
            .RequireRateLimiting("public-api");

        group.MapPost("/", CreateInquiry)
            .WithName("CreateInquiry")
            .WithDescription("Create a new property inquiry");
    }

    private static async Task<IResult> CreateInquiry(
        CreatePropertyInquiryRequest request,
        IMediator mediator,
        IValidator<CreatePropertyInquiryCommand> validator)
    {
        var command = new CreatePropertyInquiryCommand
        {
            PropertyId = request.PropertyId,
            ContactName = request.ContactName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Message = request.Message
        };

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            var errorResponse = ApiResponse<PropertyInquiryDto>.FailureResponse(
                "Validation failed",
                errors);

            return Results.BadRequest(errorResponse);
        }

        try
        {
            var result = await mediator.Send(command);
            var response = ApiResponse<PropertyInquiryDto>.SuccessResponse(
                result,
                "Inquiry created successfully");

            return Results.Created($"/api/v1/inquiries/{result.Id}", response);
        }
        catch (Exception ex)
        {
            var errorResponse = ApiResponse<PropertyInquiryDto>.FailureResponse(ex.Message);
            return Results.BadRequest(errorResponse);
        }
    }
}
