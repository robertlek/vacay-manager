#nullable disable warnings

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VM.Models;

public class Employee : IdentityUser
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    [ValidateNever]
    [ForeignKey("DepartmentId")]
    public Department Department { get; set; }
    public DateTime EmployedOn { get; set; }
}
