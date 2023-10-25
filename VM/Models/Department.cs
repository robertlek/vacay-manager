using System.ComponentModel.DataAnnotations;

namespace VM.Models;

public class Department
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please insert a valid department name.")]
    [StringLength(100, ErrorMessage = "The department's name is too long.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Please insert a valid number of overlaps.")]
    [Range(0, 30, ErrorMessage = "The overlaps range is between 0 and 30.")]
    public int Overlaps { get; set; }
}
