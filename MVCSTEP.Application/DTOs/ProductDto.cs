namespace MVCSTEP.Application.DTOs;

public record ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string UserId { get; set; }
}