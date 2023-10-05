#nullable disable warnings

using System.ComponentModel.DataAnnotations;

namespace VM.Models;

public class Department
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int Overlaps { get; set; }
}
