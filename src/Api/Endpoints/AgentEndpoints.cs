namespace WhatsReal.Api.Endpoints;

using MediatR;
using WhatsReal.Application.Features.Agents.Queries;
using WhatsReal.Shared.DTOs;

/// <summary>
/// Agent endpoints for listing and retrieving agent information.
/// </summary>
public static class AgentEndpoints
{
    public static void MapAgentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1/agents")
            .WithName("Agents")
            .WithTags("Agents")
            .RequireRateLimiting("public-api");

        group.MapGet("/", GetAllAgents)
            .WithName("GetAllAgents")
            .WithDescription("Get all agents")
            .CacheOutput();

        group.MapGet("/{id}", GetAgentById)
            .WithName("GetAgentById")
            .WithDescription("Get agent by ID with their properties")
            .CacheOutput();
    }

    private static async Task<IResult> GetAllAgents(IMediator mediator)
    {
        var query = new GetAllAgentsQuery();
        var result = await mediator.Send(query);

        var response = ApiResponse<List<AgentListDto>>.SuccessResponse(
            result,
            "Agents retrieved successfully");

        return Results.Ok(response);
    }

    private static async Task<IResult> GetAgentById(
        Guid id,
        IMediator mediator)
    {
        var query = new GetAgentByIdQuery(id);
        var agent = await mediator.Send(query);

        if (agent == null)
        {
            var errorResponse = ApiResponse<AgentDetailDto>.FailureResponse(
                $"Agent with ID {id} not found");
            return Results.NotFound(errorResponse);
        }

        var response = ApiResponse<AgentDetailDto>.SuccessResponse(
            agent,
            "Agent retrieved successfully");

        return Results.Ok(response);
    }
}
