namespace WhatsReal.Api.Endpoints;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhatsReal.Application.Features.Properties.Queries;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Property endpoints for searching, listing, and retrieving properties.
/// </summary>
public static class PropertyEndpoints
{
    public static void MapPropertyEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/properties")
            .WithName("Properties")
            .WithTags("Properties")
            .RequireRateLimiting("public-api");

        group.MapGet("/", GetAllProperties)
            .WithName("GetAllProperties")
            .WithDescription("Get all properties with pagination")
            .CacheOutput("property-search");

        group.MapGet("/{id}", GetPropertyById)
            .WithName("GetPropertyById")
            .WithDescription("Get a single property by ID")
            .CacheOutput();

        group.MapGet("/search", SearchProperties)
            .WithName("SearchProperties")
            .WithDescription("Search properties with filters")
            .CacheOutput("property-search");
    }

    private static async Task<IResult> GetAllProperties(
        IMediator mediator,
        [AsParameters] GetAllPropertiesRequest request)
    {
        var query = new GetAllPropertiesQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        var result = await mediator.Send(query);
        var response = ApiResponse<PagedResponse<PropertyDto>>.SuccessResponse(
            result,
            "Properties retrieved successfully");

        return Results.Ok(response);
    }

    private static async Task<IResult> GetPropertyById(
        Guid id,
        IMediator mediator)
    {
        var query = new GetPropertyByIdQuery(id);
        var property = await mediator.Send(query);

        if (property == null)
        {
            var errorResponse = ApiResponse<PropertyDetailDto>.FailureResponse(
                $"Property with ID {id} not found");
            return Results.NotFound(errorResponse);
        }

        var response = ApiResponse<PropertyDetailDto>.SuccessResponse(
            property,
            "Property retrieved successfully");

        return Results.Ok(response);
    }

    private static async Task<IResult> SearchProperties(
        IMediator mediator,
        [AsParameters] SearchPropertiesRequest request)
    {
        var query = new SearchPropertiesQuery
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            MinPrice = request.MinPrice,
            MaxPrice = request.MaxPrice,
            PropertyType = request.PropertyType,
            Bedrooms = request.Bedrooms,
            Bathrooms = request.Bathrooms,
            IsFurnished = request.IsFurnished,
            City = request.City,
            SortBy = request.SortBy,
            SortDirection = request.SortDirection,
            SearchTerm = request.SearchTerm
        };

        var result = await mediator.Send(query);
        var response = ApiResponse<PagedResponse<PropertyDto>>.SuccessResponse(
            result,
            "Properties searched successfully");

        return Results.Ok(response);
    }
}

/// <summary>
/// Request model for GetAllProperties endpoint.
/// </summary>
public class GetAllPropertiesRequest
{
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 20;
}

/// <summary>
/// Request model for SearchProperties endpoint.
/// </summary>
public class SearchPropertiesRequest
{
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;

    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 20;

    [FromQuery(Name = "minPrice")]
    public decimal? MinPrice { get; set; }

    [FromQuery(Name = "maxPrice")]
    public decimal? MaxPrice { get; set; }

    [FromQuery(Name = "type")]
    public string? PropertyType { get; set; }

    [FromQuery(Name = "bedrooms")]
    public int? Bedrooms { get; set; }

    [FromQuery(Name = "bathrooms")]
    public int? Bathrooms { get; set; }

    [FromQuery(Name = "furnished")]
    public bool? IsFurnished { get; set; }

    [FromQuery(Name = "city")]
    public string? City { get; set; }

    [FromQuery(Name = "sortBy")]
    public string? SortBy { get; set; }

    [FromQuery(Name = "sortDirection")]
    public string SortDirection { get; set; } = "asc";

    [FromQuery(Name = "search")]
    public string? SearchTerm { get; set; }
}
