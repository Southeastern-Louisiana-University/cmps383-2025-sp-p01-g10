using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Dtos;

public class TheaterDto
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public required string Name { get; set; }

    [Required]
    public required string Address { get; set; }

    [Required, Range(1, int.MaxValue)]
    public int SeatCount { get; set; }
}
