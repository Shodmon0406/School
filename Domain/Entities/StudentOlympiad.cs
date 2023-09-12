namespace Domain.Entities;

public class StudentOlympiad
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int OlympiadId { get; set; }
    public Student Student { get; set; }
    public Olympiad Olympiad { get; set; }
}
