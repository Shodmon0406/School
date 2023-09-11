namespace Domain.Filters.ParentFilter;

public class GetParentFilter : PaginationFilter
{
    public int Id { get; set; }
    public int ParrentCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
