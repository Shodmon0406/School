using Domain.Enums;

namespace Domain.Entities
{
    public class Stipend
    {
        public int Id { get; set; }
        public string StudenId { get; set; } = null!;
        public Student Student { get; set; } = null!;
        public StipendType StipendType { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
