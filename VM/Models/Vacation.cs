#nullable disable warnings

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VM.Models;

public class Vacation
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string EmployeeId { get; set; }
    [ValidateNever]
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    [ValidateNever]
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
    [Required]
    public DateTime FromDate { get; set; }
    [Required]
    public DateTime ToDate { get; set; }
    public DateTime CreatedOn { get; set; }
}
