using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<Class> Classes { get; set; }
    }
}
