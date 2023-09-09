namespace Domain.Dtos.Class;

public class ClassDto
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = null!;
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public int ClassroomId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}