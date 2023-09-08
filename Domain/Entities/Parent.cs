namespace Domain.Entities
{
    public class Parent
    {
        public int Id { get; set; }
        public int ParrentCode { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<StudentParrent> StudentParrents { get; set; }
    }
}
