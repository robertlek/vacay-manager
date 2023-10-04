#nullable disable warnings

using System.ComponentModel.DataAnnotations;

namespace VM.Models;

public class Department
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Overlaps { get; set; }
}
