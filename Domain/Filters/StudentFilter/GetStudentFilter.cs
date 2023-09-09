namespace Domain.Filters.StudentFilter;

public class GetStudentFilter:PaginationFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string?  Email{ get; set; }
}