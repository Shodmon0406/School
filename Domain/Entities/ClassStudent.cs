using System.Reflection.Metadata.Ecma335;

namespace Domain.Entities
{
    public class ClassStudent
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = null!;
        public Student Student { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
