using Boilerplate.Application.Common.Requests;

namespace Boilerplate.Application.Filters;

public record GetUsersFilter : PaginatedRequest
{
    public string? Email { get; init; }
    public bool IsAdmin { get; init; }
}