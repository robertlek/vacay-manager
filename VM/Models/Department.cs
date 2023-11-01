using System.ComponentModel.DataAnnotations;

namespace VM.Models;

public class Department
{
    /// <summary>
    /// Represents the id of a department and the primary key of the table.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Represents the name of a department.
    /// </summary>
    [Required(ErrorMessage = "Please insert a valid department name.")]
    [StringLength(100, ErrorMessage = "The department's name is too long.")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Represents the maximum overlaps of active vacations in the same department.
    /// </summary>
    [Required(ErrorMessage = "Please insert a valid number of overlaps.")]
    [Range(0, 30, ErrorMessage = "The overlaps range is between 0 and 30.")]
    public int Overlaps { get; set; }
}
