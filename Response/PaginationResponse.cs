using PaginationAndFiltering.Filter;

namespace PaginationAndFiltering.Response;

public class PaginationResponse<T> : BaseFilter
{
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public T Data { get; set; }

    private PaginationResponse(int pageNumber, int pageSize, int totalRecords, T data) 
        : base(pageSize, pageNumber)
    {
        Data = data;
        TotalRecords = totalRecords;
        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public static PaginationResponse<T> Create(int pageNumber, int pageSize, int totalRecords, T data)
        => new PaginationResponse<T>(pageNumber, pageSize, totalRecords, data);
}