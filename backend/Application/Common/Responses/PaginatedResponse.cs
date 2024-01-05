using System;

namespace Application.Common.Responses
{
    public class PaginatedResponse<T> : CommandResponse<IReadOnlyList<T>>
{
    public int PageNumber  { get; set; }
    public int PageSize {get;set;}
    public int Count { get; set; }
}
}