namespace Domain.Entities;

public class Olympiad
{
    public int Id { get; set; }
    public Student Student { get; set; }
    public string Discription { get; set; }
    public string Subject { get; set; }
    public string Award { get; set; }
    public DateTime Year { get; set; }

}
