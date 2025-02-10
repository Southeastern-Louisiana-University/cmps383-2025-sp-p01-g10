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

   public int SeatCount { get; set; }
}
