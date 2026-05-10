namespace WhatsReal.Shared.DTOs;

/// <summary>
/// Generic API response wrapper for all endpoints.
/// </summary>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Operation successful")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
            Errors = null
        };
    }

    public static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Data = default,
            Message = message,
            Errors = errors ?? []
        };
    }
}

/// <summary>
/// Generic paged response for paginated data.
/// </summary>
public class PagedResponse<T>
{
    public List<T> Items { get; set; } = [];
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    public static PagedResponse<T> Create(
        List<T> items, 
        int totalCount, 
        int pageNumber, 
        int pageSize)
    {
        return new PagedResponse<T>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}

/// <summary>
/// Response wrapping a paged result.
/// </summary>
public class ApiResponse<T, TPaged> where TPaged : class
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public TPaged? PagedData { get; set; }
    public List<string>? Errors { get; set; }
}
