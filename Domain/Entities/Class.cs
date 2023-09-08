namespace Domain.Entities
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = null!;
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<ClassStudent> ClassStudents { get; set; }
    }
}
