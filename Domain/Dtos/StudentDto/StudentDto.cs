using Domain.Enums;

namespace Domain.Dtos.Student;

public class StudentDto
{
   
    public string? StudentCode { get; set; } 
    public string? FirstName { get; set; } 
    public string? LastName { get; set; } 
    public string? Email { get; set; } 
    public string? PhoneNumber { get; set; } 
    public Gender Gender { get; set; }
    public DateTime DOB { get; set; }
    public string? Address  { get; set; }
    public int Stage { get; set; }
    public bool Active { get; set; } = true;
    public DateTime JoinDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}