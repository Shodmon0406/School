using Domain.Entities;
namespace Domain.Dtos.OlympiadDtos;

public class OlympiadDto
{
    public int Id { get; set; }
    public Entities.Student Student { get; set; }
    public string Discription { get; set; }
    public string Subject { get; set; }
    public string Award { get; set; }
    public DateTime Year { get; set; }
}
