namespace Domain.Entities
{
    public class StudentParrent
    {
        public int Id { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
