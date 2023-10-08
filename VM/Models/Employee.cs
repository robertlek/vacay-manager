#nullable disable warnings

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VM.Models;

public class Employee : IdentityUser
{
    [Required(ErrorMessage = "Please insert a valid first name.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please insert a valid last name.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please select a valid department.")]
    public int DepartmentId { get; set; }
    [ValidateNever]
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
    public DateTime EmployedOn { get; set; }
}
