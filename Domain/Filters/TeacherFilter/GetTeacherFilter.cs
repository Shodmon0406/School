namespace Domain.Filters.TeacherFilter;

public class GetTeacherFilter:PaginationFilter
{
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
}