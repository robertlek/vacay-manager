#nullable disable warnings

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VM.Models;

public class Vacation
{
    /// <summary>
    /// Represents the id of a vacation and the primary key of the table.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Represents the id of the employee who created the vacation.
    /// </summary>
    [Required]
    public string EmployeeId { get; set; }

    /// <summary>
    /// Represents the employee who created the vacation.
    /// </summary>
    [ValidateNever]
    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }

    /// <summary>
    /// Represents the id of the department the employee that created the vacation is working in.
    /// </summary>
    [Required]
    public int DepartmentId { get; set; }

    /// <summary>
    /// Represents the department the employee that created the vacation is working in.
    /// </summary>
    [ValidateNever]
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }

    /// <summary>
    /// Represents the start date of a vacation.
    /// </summary>
    [Required]
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Represents the end date of a vacation.
    /// </summary>
    [Required]
    public DateTime ToDate { get; set; }

    /// <summary>
    /// Represents the date when a vacation was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }
}
