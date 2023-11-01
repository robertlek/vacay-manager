#nullable disable warnings

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VM.Models;

public class Employee : IdentityUser
{
    /// <summary>
    /// Represents the first name of an employee.
    /// </summary>
    [Required(ErrorMessage = "Please insert a valid first name.")]
    public string FirstName { get; set; }

    /// <summary>
    /// Represents the last name of an employee.
    /// </summary>
    [Required(ErrorMessage = "Please insert a valid last name.")]
    public string LastName { get; set; }

    /// <summary>
    /// Represents the id of the department the employee is working in.
    /// </summary>
    [Required(ErrorMessage = "Please select a valid department.")]
    public int DepartmentId { get; set; }

    /// <summary>
    /// Represents the department the employee is working in.
    /// </summary>
    [ValidateNever]
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }

    /// <summary>
    /// Represents the employing date of the employer.
    /// </summary>
    public DateTime EmployedOn { get; set; }
}
