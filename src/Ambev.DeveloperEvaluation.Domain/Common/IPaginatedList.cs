namespace Ambev.DeveloperEvaluation.Domain.Common;

public interface IPaginatedList<T> : IList<T>
{
    int CurrentPage { get; set; }
    int TotalPages { get; set; }
    int PageSize { get; set; }
    int TotalCount { get; set; }
    bool HasPrevious { get; }
    bool HasNext { get; }
}