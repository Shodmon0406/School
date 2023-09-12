namespace Domain.Dtos.Student;

public class SubjectDto
{
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = null!;
    public int Stage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    
}