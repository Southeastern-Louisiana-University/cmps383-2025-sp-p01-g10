namespace Selu383.SP25.Api.Dtos;

public class TheaterDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Address { get; set; }

    public int SeatCount { get; set; }
}
