#nullable disable warnings

using VM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VM.ViewModels;

public class EmployeeUpsertViewModel
{
    [ValidateNever]
    public IEnumerable<SelectListItem> Departments { get; set; }

    public Employee Employee { get; set; }

    [Required(ErrorMessage = "Please select a valid role from the list.")]
    public string Role { get; set; }

    [ValidateNever]
    public IEnumerable<SelectListItem> Roles { get; set; }

    public User User { get; set; }
}
