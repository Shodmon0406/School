namespace Domain.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int Stage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<Class> Classes { get; set; }
        public List<ClassStudent> ClassesStudents { get; set; }
    }
}
