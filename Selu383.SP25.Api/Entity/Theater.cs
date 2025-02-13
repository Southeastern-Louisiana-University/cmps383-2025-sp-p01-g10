using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Entities;

public class Theater
{
    [Key]
    public int Id { get; set; }

    [Required,MaxLength(120)]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required,Range(1, int.MaxValue, ErrorMessage = "SeatCount must be at least 1.")]
    public int SeatCount { get; set; }
}
