namespace PaginationAndFiltering.Filter;

public class StudentFilter : BaseFilter
{
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}