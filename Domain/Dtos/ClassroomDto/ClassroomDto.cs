namespace Domain.Dtos.Classroom;

public class ClassroomDto
{
    public int ClassroomId { get; set; }
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}